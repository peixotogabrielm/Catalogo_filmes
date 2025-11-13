using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CatalogoFilmes.Documentacao
{
    /// <summary>
    /// Filtro inteligente que mapeia automaticamente exemplos de response
    /// baseado em convenções de nome, sem precisar de annotations nos controllers
    /// </summary>
    public class SmartResponseExamplesFilter : IOperationFilter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _exampleProviders;

        public SmartResponseExamplesFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            // Carrega todos os IExamplesProvider da assembly
            _exampleProviders = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && 
                    i.GetGenericTypeDefinition() == typeof(IExamplesProvider<>)))
                .ToDictionary(t => t.Name, t => t);
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses == null)
                operation.Responses = new OpenApiResponses();

            var methodName = context.MethodInfo.Name;
            var controllerName = context.MethodInfo.DeclaringType?.Name.Replace("Controller", "") ?? "";
            
            // Adiciona respostas de erro comuns se não existirem
            AddCommonErrorResponses(operation, methodName, context);

            // Adiciona exemplos para cada status code
            foreach (var statusCode in operation.Responses.Keys.ToList())
            {
                var response = operation.Responses[statusCode];
                
                // Mapeia exemplos baseado no status code e nome do método
                var exampleType = FindExampleType(methodName, statusCode, controllerName);
                
                if (exampleType != null)
                {
                    var example = CreateExampleFromType(exampleType);
                    if (example != null)
                    {
                        // Garante que o Content existe
                        if (response.Content == null)
                        {
                            response.Content = new Dictionary<string, OpenApiMediaType>();
                        }
                        
                        // Garante que application/json existe no Content
                        if (!response.Content.ContainsKey("application/json"))
                        {
                            response.Content["application/json"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object"
                                }
                            };
                        }
                        
                        var content = response.Content["application/json"];
                        
                        // Adiciona o exemplo
                        content.Examples = new Dictionary<string, OpenApiExample>
                        {
                            ["default"] = new OpenApiExample
                            {
                                Summary = GetExampleSummary(methodName, statusCode),
                                Value = example
                            }
                        };
                    }
                }
            }
        }
        
        private void AddCommonErrorResponses(OpenApiOperation operation, string methodName, OperationFilterContext context)
        {
            // Verifica se o endpoint requer autenticação
            var requiresAuth = context.MethodInfo
                .GetCustomAttributes(true)
                .Concat(context.MethodInfo.DeclaringType?.GetCustomAttributes(true) ?? Array.Empty<object>())
                .Any(attr => attr.GetType().Name == "AuthorizeAttribute");
            
            var allowAnonymous = context.MethodInfo
                .GetCustomAttributes(true)
                .Any(attr => attr.GetType().Name == "AllowAnonymousAttribute");
            
            // Adiciona 400 Bad Request para todos os endpoints que recebem dados
            if (!operation.Responses.ContainsKey("400"))
            {
                operation.Responses["400"] = new OpenApiResponse
                {
                    Description = "Requisição inválida",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema { Type = "object" }
                        }
                    }
                };
            }
            
            // Adiciona 401 Unauthorized para endpoints autenticados
            if (requiresAuth && !allowAnonymous && !operation.Responses.ContainsKey("401"))
            {
                operation.Responses["401"] = new OpenApiResponse
                {
                    Description = "Não autorizado",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema { Type = "object" }
                        }
                    }
                };
            }
            
            // Adiciona 403 Forbidden para endpoints que requerem role Admin
            var requiresAdminRole = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .Concat(context.MethodInfo.GetCustomAttributes(true))
                .Any(attr => attr.GetType().Name == "AuthorizeAttribute" && 
                            (attr.ToString()?.Contains("Admin") ?? false)) ?? false;
            
            if (requiresAdminRole && !operation.Responses.ContainsKey("403"))
            {
                operation.Responses["403"] = new OpenApiResponse
                {
                    Description = "Acesso negado",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema { Type = "object" }
                        }
                    }
                };
            }
            
            // Adiciona 404 Not Found para endpoints Get/Delete por ID
            if ((methodName.Contains("ById") || methodName.StartsWith("Delete") || methodName.StartsWith("Get")) 
                && !operation.Responses.ContainsKey("404"))
            {
                operation.Responses["404"] = new OpenApiResponse
                {
                    Description = "Não encontrado",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema { Type = "object" }
                        }
                    }
                };
            }
        }

        private Type? FindExampleType(string methodName, string statusCode, string controllerName)
        {
            // Mapeamento explícito de métodos problemáticos conhecidos
            var explicitMappings = new Dictionary<string, Dictionary<string, string>>
            {
                ["AddFilme"] = new Dictionary<string, string> { ["201"] = "FilmeCriadoResponseExample" },
                ["AddUserCatalogo"] = new Dictionary<string, string> { ["201"] = "CatalogoCriadoResponseExample" },
                ["Registrar"] = new Dictionary<string, string> { ["201"] = "RegistroResponseExample" },
                ["GetCatalogosById"] = new Dictionary<string, string> { ["200"] = "CatalogoResponseExample" },
                ["LikeCatalogo"] = new Dictionary<string, string> { ["200"] = "CatalogoLikeResponseExample" },
                ["DislikeCatalogo"] = new Dictionary<string, string> { ["200"] = "CatalogoDislikeResponseExample" },
                ["GetAllTags"] = new Dictionary<string, string> { ["200"] = "TagsResponseExample" },
                ["GetUsuarios"] = new Dictionary<string, string> { ["200"] = "UsuariosListResponseExample" },
                ["GetFilmesStats"] = new Dictionary<string, string> { ["200"] = "FilmesStatsResponseExample" },
                ["GetDashboardStats"] = new Dictionary<string, string> { ["200"] = "DashboardResponseExample" },
                ["GetNumeroFavoritos"] = new Dictionary<string, string> { ["200"] = "NumeroFavoritosResponseExample" }
            };
            
            // Verifica primeiro o mapeamento explícito
            if (explicitMappings.ContainsKey(methodName) && 
                explicitMappings[methodName].ContainsKey(statusCode))
            {
                var exampleName = explicitMappings[methodName][statusCode];
                if (_exampleProviders.ContainsKey(exampleName))
                {
                    return _exampleProviders[exampleName];
                }
            }
            
            // Normaliza o nome do método removendo palavras comuns que não fazem parte da entidade
            var normalizedMethodName = methodName
                .Replace("User", "")      // AddUserCatalogo -> AddCatalogo
                .Replace("Stats", "")     // GetDashboardStats -> GetDashboard
                .Replace("ById", "");     // GetFilmeById -> GetFilme
            
            // Mapeamento de status codes para sufixos de exemplo
            var statusMappings = new Dictionary<string, List<string>>
            {
                ["200"] = new List<string> { "Response", "" },
                ["201"] = new List<string> { "Criado", "Created" },
                ["204"] = new List<string> { "Deletado", "Atualizado", "Deleted", "Updated" },
                ["400"] = new List<string> { "BadRequest" },
                ["401"] = new List<string> { "Unauthorized" },
                ["403"] = new List<string> { "Forbidden" },
                ["404"] = new List<string> { "NotFound" },
                ["409"] = new List<string> { "Conflict" }
            };

            // Tenta encontrar um exemplo específico para o método e status
            if (statusMappings.ContainsKey(statusCode))
            {
                foreach (var suffix in statusMappings[statusCode])
                {
                    // Padrões de busca
                    var patterns = new List<string>();
                    
                    // Ex: "LoginResponseExample" para Login + 200
                    if (!string.IsNullOrEmpty(suffix))
                    {
                        patterns.Add($"{methodName}{suffix}ResponseExample");
                        patterns.Add($"{normalizedMethodName}{suffix}ResponseExample");
                    }
                    else
                    {
                        patterns.Add($"{methodName}ResponseExample");
                        patterns.Add($"{normalizedMethodName}ResponseExample");
                    }
                    
                    // Ex: "FilmeResponseExample" para GetFilmeById
                    if (methodName.StartsWith("Get") && statusCode == "200")
                    {
                        var entityName = normalizedMethodName.Replace("Get", "").Replace("All", "");
                        
                        if (methodName.Contains("All"))
                        {
                            patterns.Add($"{entityName}sListResponseExample");
                            patterns.Add($"{entityName}ListResponseExample");
                        }
                        else
                        {
                            patterns.Add($"{entityName}ResponseExample");
                        }
                    }
                    
                    // Ex: "FilmeCriadoResponseExample" para AddFilme + 201
                    // Ou "CatalogoCriadoResponseExample" para AddUserCatalogo + 201
                    if ((methodName.StartsWith("Add") || normalizedMethodName.StartsWith("Add")) && statusCode == "201")
                    {
                        var entityName = normalizedMethodName.Replace("Add", "");
                        patterns.Add($"{entityName}CriadoResponseExample");
                    }
                    
                    // Ex: "RegistroResponseExample" para Registrar + 201
                    if (methodName.Equals("Registrar") && statusCode == "201")
                    {
                        patterns.Add("RegistroResponseExample");
                    }
                    
                    // Ex: "FilmeAtualizadoResponseExample" para UpdateFilme + 200
                    // Ou "CatalogoAtualizadoResponseExample" para UpdateUserCatalogo + 200
                    if ((methodName.StartsWith("Update") || normalizedMethodName.StartsWith("Update")) && statusCode == "200")
                    {
                        var entityName = normalizedMethodName.Replace("Update", "");
                        patterns.Add($"{entityName}AtualizadoResponseExample");
                        
                        // Caso específico: UpdateUserRole -> RoleAtualizadaResponseExample
                        if (methodName.Contains("Role"))
                        {
                            patterns.Add("RoleAtualizadaResponseExample");
                        }
                    }
                    
                    // Ex: "FilmeDeletadoResponseExample" para DeleteFilme + 200
                    // Ou "CatalogoDeletadoResponseExample" para DeleteUserCatalogo + 200
                    if ((methodName.StartsWith("Delete") || normalizedMethodName.StartsWith("Delete")) && (statusCode == "200" || statusCode == "204"))
                    {
                        var entityName = normalizedMethodName.Replace("Delete", "");
                        patterns.Add($"{entityName}DeletadoResponseExample");
                    }
                    
                    // Busca por padrões usando case-insensitive
                    foreach (var pattern in patterns)
                    {
                        // Busca exata primeiro
                        if (_exampleProviders.ContainsKey(pattern))
                        {
                            return _exampleProviders[pattern];
                        }
                        
                        // Busca case-insensitive como fallback
                        var found = _exampleProviders.Keys.FirstOrDefault(k => 
                            k.Equals(pattern, StringComparison.OrdinalIgnoreCase));
                        
                        if (found != null)
                        {
                            return _exampleProviders[found];
                        }
                    }
                }
            }

            // Retorna exemplo de erro genérico para códigos de erro
            if (int.TryParse(statusCode, out int code) && code >= 400)
            {
                var errorExamples = new Dictionary<int, string>
                {
                    [400] = "BadRequestResponseExample",
                    [401] = "UnauthorizedResponseExample",
                    [403] = "ForbiddenResponseExample",
                    [404] = "NotFoundResponseExample",
                    [409] = "ConflictResponseExample"
                };

                if (errorExamples.ContainsKey(code) && _exampleProviders.ContainsKey(errorExamples[code]))
                {
                    return _exampleProviders[errorExamples[code]];
                }
            }

            return null;
        }

        private IOpenApiAny? CreateExampleFromType(Type exampleType)
        {
            try
            {
                var instance = Activator.CreateInstance(exampleType);
                var method = exampleType.GetMethod("GetExamples");
                
                if (method != null)
                {
                    var exampleData = method.Invoke(instance, null);
                    if (exampleData != null)
                    {
                        return ConvertToOpenApiAny(exampleData);
                    }
                }
            }
            catch (Exception)
            {
                // Se falhar, retorna null e o Swagger usa o schema padrão
            }

            return null;
        }

        private IOpenApiAny ConvertToOpenApiAny(object obj)
        {
            if (obj == null)
                return new OpenApiNull();

            if (obj is string str)
                return new OpenApiString(str);

            if (obj is int i)
                return new OpenApiInteger(i);

            if (obj is long l)
                return new OpenApiLong(l);

            if (obj is float f)
                return new OpenApiFloat(f);

            if (obj is double d)
                return new OpenApiDouble(d);

            if (obj is bool b)
                return new OpenApiBoolean(b);

            if (obj is DateTime dt)
                return new OpenApiDateTime(dt);

            if (obj is Guid guid)
                return new OpenApiString(guid.ToString());

            if (obj is Enum enumValue)
                return new OpenApiString(enumValue.ToString());

            // Arrays e Listas
            if (obj is System.Collections.IEnumerable enumerable && !(obj is string))
            {
                var array = new OpenApiArray();
                foreach (var item in enumerable)
                {
                    array.Add(ConvertToOpenApiAny(item));
                }
                return array;
            }

            // Objetos complexos
            var openApiObject = new OpenApiObject();
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                try
                {
                    var value = prop.GetValue(obj);
                    if (value != null)
                    {
                        var propName = char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1); // camelCase
                        openApiObject[propName] = ConvertToOpenApiAny(value);
                    }
                }
                catch
                {
                    // Ignora propriedades que não podem ser lidas
                }
            }

            return openApiObject;
        }

        private string GetExampleSummary(string methodName, string statusCode)
        {
            var summaries = new Dictionary<string, string>
            {
                ["200"] = "Sucesso",
                ["201"] = "Criado com sucesso",
                ["204"] = "Operação realizada com sucesso",
                ["400"] = "Requisição inválida",
                ["401"] = "Não autorizado",
                ["403"] = "Acesso negado",
                ["404"] = "Não encontrado",
                ["409"] = "Conflito"
            };

            return summaries.ContainsKey(statusCode) 
                ? $"{summaries[statusCode]} - {methodName}" 
                : $"Exemplo para status {statusCode}";
        }
    }
}
