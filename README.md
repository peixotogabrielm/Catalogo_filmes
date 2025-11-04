# 🎬 API Catálogo de Filmes

Uma API RESTful desenvolvida em .NET 8 para gerenciamento de um catálogo de filmes com sistema de autenticação e autorização baseado em JWT.

## ✨ Funcionalidades

### 🔐 Autenticação e Autorização
- **Login/Registro** de usuários
- **Autenticação JWT** para proteção de endpoints
- **Sistema de Roles** (User/Admin)
- **Registro de administradores** com chave secreta

### 🎭 Gerenciamento de Filmes
- **CRUD completo** de filmes (Create, Read, Update, Delete)
- **Filtros de busca** por título, gênero e ano
- **Paginação** de resultados
- **Validação** de dados de entrada

### 👥 Painel Administrativo
- **Dashboard** com estatísticas do sistema
- **Gerenciamento de usuários** (listar, atualizar roles, deletar)
- **Estatísticas de filmes** por gênero e ano
- **Controle de acesso** restrito a administradores

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - Para criação da API REST
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados
- **JWT (JSON Web Tokens)** - Autenticação e autorização
- **BCrypt** - Hash de senhas
- **Swagger/OpenAPI** - Documentação da API
- **AutoMapper** - Mapeamento de objetos (implícito nos DTOs)

## 📋 Pré-requisitos

- **.NET 8 SDK** ([Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0))
- **SQL Server** (Local, LocalDB ou Azure)
- **Visual Studio Code** com as extensões:
  - C# Dev Kit
  - C# Extension
  - .NET Extension Pack

## ⚙️ Configuração e Instalação

### 1. Clone o repositório
```bash
git clone https://github.com/peixotogabrielm/Catalogo_filmes.git
cd Catalogo_filmes
```

### 2. Configure o banco de dados
Edite o arquivo `CatalogoFilmes/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CatalogoFilmesDB;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Secret": "minha-super-chave-secreta-jwt-2024-catalogo-filmes-api",
    "Issuer": "CatalogoFilmesAPI",
    "Audience": "CatalogoFilmesUsers"
  },
  "AdminSecretKey": "lM3ULXRHup"
}
```

### 3. Restaure as dependências
```bash
cd CatalogoFilmes
dotnet restore
```

### 4. Execute as migrações do banco
```bash
dotnet ef database update
```

### 5. Execute a aplicação

#### Opção A: Terminal (Simples)
```bash
dotnet run
```

#### Opção B: VS Code (Recomendado)
1. Abra o projeto no VS Code
2. Pressione `Ctrl+Shift+P` e digite ">.NET: Generate Assets for Build and Debug"
3. Pressione `F5` para executar com debug, ou `Ctrl+F5` para executar sem debug

A API estará disponível em:
- **HTTP**: `http://localhost:5103`
- **HTTPS**: `https://localhost:7224`
- **Swagger UI**: `https://localhost:7224/swagger`

## � Configuração do VS Code

### Arquivos de Configuração Necessários

O projeto já inclui os arquivos de configuração para VS Code:

#### `.vscode/launch.json` (Debug Configuration)
```json
{
  "configurations": [
    {
      "name": "Launch CatalogoFilmes",
      "type": "coreclr",
      "request": "launch",
      "program": "${workspaceFolder}/CatalogoFilmes/bin/Debug/net8.0/CatalogoFilmes.dll",
      "args": [],
      "cwd": "${workspaceFolder}/CatalogoFilmes",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "preLaunchTask": "build"
    }
  ]
}
```

#### `.vscode/tasks.json` (Build Tasks)
```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "command": "dotnet",
      "type": "process",
      "args": [
        "build",
        "${workspaceFolder}/CatalogoFilmes/CatalogoFilmes.csproj",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "group": "build",
      "presentation": {
        "reveal": "silent"
      },
      "problemMatcher": "$msCompile"
    },
    {
      "label": "run",
      "command": "dotnet",
      "type": "process",
      "args": [
        "run",
        "--project",
        "${workspaceFolder}/CatalogoFilmes/CatalogoFilmes.csproj"
      ],
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "presentation": {
        "echo": true,
        "reveal": "always",
        "focus": false,
        "panel": "shared"
      },
      "problemMatcher": "$msCompile"
    }
  ]
}
```

### Como Executar no VS Code

1. **F5** - Executar com Debug (recomendado para desenvolvimento)
2. **Ctrl+F5** - Executar sem Debug (mais rápido)
3. **Ctrl+Shift+P** → "Tasks: Run Task" → "run" - Executar via task

## 🔍 Troubleshooting

### Problemas Comuns

#### 1. "dotnet command not found"
```bash
# Verifique se o .NET 8 SDK está instalado
dotnet --version

# Se não estiver instalado, baixe em:
# https://dotnet.microsoft.com/download/dotnet/8.0
```

#### 2. Erro de conexão com banco de dados
```bash
# Verifique se o SQL Server LocalDB está rodando
sqllocaldb info mssqllocaldb

# Se não estiver, inicie:
sqllocaldb start mssqllocaldb
```

#### 3. Erro de certificado HTTPS
```bash
# Confie no certificado de desenvolvimento
dotnet dev-certs https --trust
```

#### 4. Porta já em uso
- Altere as portas em `Properties/launchSettings.json`
- Ou pare outros processos usando as portas 5103/7224

#### 5. Problemas com JWT
- Verifique se a chave secreta no `appsettings.Development.json` tem pelo menos 32 caracteres
- Certifique-se de que não há espaços extras na configuração

### Logs e Debug

Para ver logs detalhados, edite o `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

## �📚 Documentação da API

### URLs Base
- **Development HTTP**: `http://localhost:5103/api`
- **Development HTTPS**: `https://localhost:7224/api`
- **Swagger UI**: `https://localhost:7224/swagger`

### 🔓 Endpoints Públicos

#### Autenticação

**POST** `/auth/login`
```json
{
  "email": "usuario@email.com",
  "senha": "senha123"
}
```

**POST** `/auth/registrar`
```json
{
  "nome": "Nome do Usuário",
  "email": "usuario@email.com",
  "senha": "senha123"
}
```

**POST** `/admin/RegistrarAdmin?chaveSecreta=lM3ULXRHup`
```json
{
  "nome": "Admin",
  "email": "admin@email.com",
  "senha": "senha123"
}
```

### 🔒 Endpoints Protegidos (Requer JWT)

#### Filmes

**GET** `/filmes/GetAllFilmes`
- Query Parameters: `titulo`, `genero`, `ano`, `pageNumber`, `pageSize`

**GET** `/filmes/GetFilmeById/{id}`

**POST** `/filmes/AddFilme`
```json
{
  "titulo": "Nome do Filme",
  "genero": "Ação",
  "ano": 2024,
  "sinopse": "Descrição do filme..."
}
```

**PUT** `/filmes/UpdateFilme`
```json
{
  "id": "guid-do-filme",
  "titulo": "Nome Atualizado",
  "genero": "Drama",
  "ano": 2024,
  "sinopse": "Nova descrição..."
}
```

**DELETE** `/filmes/DeleteFilme/{id}`

### 👑 Endpoints Administrativos (Requer Role Admin)

#### Dashboard
**GET** `/admin/Dashboard` - Estatísticas gerais do sistema

#### Gerenciamento de Usuários
**GET** `/admin/Usuarios?pageNumber=1&pageSize=20` - Lista usuários com paginação

**PUT** `/admin/Usuarios/role` - Atualiza role do usuário
```json
{
  "usuarioId": "guid-do-usuario",
  "novaRole": "Admin"
}
```

**DELETE** `/admin/Usuarios/{id}` - Remove usuário

#### Estatísticas de Filmes
**GET** `/admin/Filmes/stats` - Estatísticas de filmes por gênero e ano

## 🔑 Autenticação

### Como usar JWT

1. **Faça login** para obter o token:
```bash
curl -X POST "https://localhost:7224/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"seu@email.com","senha":"suasenha"}'
```

2. **Use o token** em requisições protegidas:
```bash
curl -X GET "https://localhost:7224/api/filmes/GetAllFilmes" \
  -H "Authorization: Bearer SEU_TOKEN_JWT_AQUI"
```

### Swagger UI
Acesse `https://localhost:7224/swagger` para testar a API interativamente.

## 📊 Estrutura de Resposta

### Padrão de Response da API

Todas as respostas seguem o padrão `Result<T>`:

```json
{
  "success": true,
  "mensagem": "Operação realizada com sucesso",
  "data": {
    // Dados retornados aqui
  }
}
```

### Resposta de Erro

```json
{
  "success": false,
  "mensagem": "Descrição do erro",
  "data": null
}
```

### Respostas Paginadas

```json
{
  "success": true,
  "mensagem": "Filmes recuperados com sucesso",
  "data": {
    "items": [...],
    "totalCount": 100,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

## 📊 Modelo de Dados

### Filme
```csharp
public class Filme
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }        // Obrigatório
    public string Genero { get; set; }        // Obrigatório
    public int Ano { get; set; }              // Obrigatório
    public string Sinopse { get; set; }
}
```

### Usuario
```csharp
public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; }          // Obrigatório
    public string Email { get; set; }         // Obrigatório
    public string SenhaHash { get; set; }     // Obrigatório
    public string Role { get; set; }          // "User" ou "Admin"
    public DateTime DataCriacao { get; set; }
}
```

## 🧪 Testando a API

### Primeiro Uso - Criando um Administrador

1. **Registre um administrador** (usando a chave secreta):
```bash
curl -X POST "https://localhost:7224/api/admin/RegistrarAdmin?chaveSecreta=lM3ULXRHup" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Admin Principal",
    "email": "admin@catalogo.com",
    "senha": "Admin123!"
  }'
```

2. **Faça login** para obter o token JWT:
```bash
curl -X POST "https://localhost:7224/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@catalogo.com",
    "senha": "Admin123!"
  }'
```

3. **Use o token** para acessar endpoints protegidos:
```bash
curl -X GET "https://localhost:7224/api/admin/Dashboard" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

### Testando com Swagger UI

1. Abra `https://localhost:7224/swagger`
2. Clique em "Authorize" (🔒)
3. Digite: `Bearer SEU_TOKEN_JWT`
4. Agora você pode testar todos os endpoints!

## 🧪 Exemplos de Uso

### Registrar e fazer login
```javascript
// 1. Registrar usuário
const registro = await fetch('/api/auth/registrar', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    nome: 'João Silva',
    email: 'joao@email.com',
    senha: 'senha123'
  })
});

// 2. Fazer login
const login = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'joao@email.com',
    senha: 'senha123'
  })
});

const { token } = await login.json();
```

### Adicionar um filme
```javascript
const novoFilme = await fetch('/api/filmes/AddFilme', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    titulo: 'Inception',
    genero: 'Ficção Científica',
    ano: 2010,
    sinopse: 'Dom Cobb é um ladrão especializado...'
  })
});
```

### Buscar filmes com filtros
```javascript
const filmes = await fetch('/api/filmes/GetAllFilmes?genero=Ação&ano=2023&pageSize=10', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});
```

## 🔧 Estrutura do Projeto

```
CatalogoFilmes/
├── Controllers/          # Controladores da API
│   ├── AuthController.cs
│   ├── FilmesController.cs
│   └── AdminController.cs
├── Models/              # Modelos de dados
│   ├── Filme.cs
│   └── Usuario.cs
├── DTOs/               # Data Transfer Objects
├── Services/           # Lógica de negócio
├── Repositories/       # Acesso a dados
├── Data/              # Contexto do Entity Framework
├── Helpers/           # Classes auxiliares (JWT, etc.)
└── Migrations/        # Migrações do banco de dados
```

## 🛡️ Segurança

- **Senhas** são hasheadas com BCrypt
- **JWT** com tempo de expiração configurável
- **Validação** de entrada em todos os endpoints
- **Autorização** baseada em roles
- **CORS** configurado para desenvolvimento

## 🚀 Deploy

### Usando Docker (Opcional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CatalogoFilmes.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogoFilmes.dll"]
```


## 👨‍💻 Autor

**Gabriel Peixoto**
- GitHub: [@peixotogabrielm](https://github.com/peixotogabrielm)

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

⭐ Se este projeto foi útil para você, considere dar uma estrela!
