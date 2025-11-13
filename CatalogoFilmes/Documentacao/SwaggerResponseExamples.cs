using System;
using System.Collections.Generic;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CatalogoFilmes.Documentacao
{
    // ========== AUTENTICAÇÃO - RESPOSTAS ==========

    public class LoginResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            data = new
            {
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvYW8gU2lsdmEiLCJpYXQiOjE1MTYyMzkwMjJ9.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class RegistroResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 201,
            message = "Usuário registrado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    // ========== FILMES - RESPOSTAS ==========

    public class FilmeResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new FilmeDTO
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Titulo = "A Origem",
                Generos = "Ficção Científica, Ação",
                Ano = "2010",
                Sinopse = "Um ladrão que rouba segredos corporativos através do uso da tecnologia de compartilhamento de sonhos.",
                Duracao = 148,
                Idioma = "Inglês",
                PosterUrl = "https://example.com/poster.jpg"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class FilmesListResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new ResultadoPaginaDTO<FilmeDTO>
            {
                Data = new List<FilmeDTO>
                {
                    new FilmeDTO
                    {
                        Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        Titulo = "A Origem",
                        Generos = "Ficção Científica, Ação",
                        Ano = "2010",
                        Sinopse = "Um ladrão que rouba segredos corporativos através do uso da tecnologia de compartilhamento de sonhos.",
                        Duracao = 148,
                        Idioma = "Inglês",
                        PosterUrl = "https://example.com/poster1.jpg"
                    },
                    new FilmeDTO
                    {
                        Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                        Titulo = "Matrix",
                        Generos = "Ficção Científica, Ação",
                        Ano = "1999",
                        Sinopse = "Um hacker descobre a verdade sobre sua realidade e seu papel na guerra contra seus controladores.",
                        Duracao = 136,
                        Idioma = "Inglês",
                        PosterUrl = "https://example.com/poster2.jpg"
                    }
                },
                TotalItems = 25,
                PageNumber = 1,
                PageSize = 10
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class FilmeCriadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 201,
            message = "Filme criado com sucesso",
            data = new FilmeDTO
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Titulo = "A Origem",
                Generos = "Ficção Científica, Ação",
                Ano = "2010",
                Duracao = 148
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class FilmeAtualizadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Filme atualizado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    public class FilmeDeletadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Filme deletado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    // ========== CATÁLOGO - RESPOSTAS ==========

    public class CatalogoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new CatalogoDTO
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Nome = "Melhores Filmes de Ficção Científica",
                Descricao = "Uma coleção dos melhores filmes do gênero ficção científica",
                Visualizacoes = 1523,
                Likes = 342,
                Dislikes = 12,
                NumeroFavoritos = 89,
                Tags = Tags.FiccaoCientifica,
                NomeUsuario = "João Silva",
                Filmes = new List<Filme>()
                {
                    new Filme
                    {
                        Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        Titulo = "Matrix",
                        Genero = "Ficção Científica, Ação",
                        Ano = "1999",
                        Duracao = 136
                    },
                    new Filme
                    {
                        Id = Guid.Parse("1c6b147e-3e6f-4d2a-9f3a-2f4e5d6c7b8a"),
                        Titulo = "Interstellar",
                        Genero = "Ficção Científica, Aventura",
                        Ano = "2014",
                        Duracao = 169
                    }

                }     
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogosListResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new List<CatalogoDTO>
            {
                new CatalogoDTO
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Nome = "Melhores Filmes de Ficção Científica",
                    Descricao = "Uma coleção dos melhores filmes do gênero ficção científica",
                    Visualizacoes = 1523,
                    Likes = 342,
                    Dislikes = 12,
                    NumeroFavoritos = 89,
                    Tags = Tags.FiccaoCientifica,
                    NomeUsuario = "João Silva",
                    Filmes = new List<Filme>()
                },
                new CatalogoDTO
                {
                    Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Nome = "Clássicos do Cinema",
                    Descricao = "Filmes que marcaram época",
                    Visualizacoes = 2341,
                    Likes = 567,
                    Dislikes = 23,
                    NumeroFavoritos = 123,
                    Tags = Tags.Drama | Tags.Romance,
                    NomeUsuario = "Maria Santos",
                    Filmes = new List<Filme>()
                }
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogoCriadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 201,
            message = "Catálogo criado com sucesso",
            data = new
            {
                id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                nome = "Melhores Filmes de Ficção Científica",
                descricao = "Uma coleção dos melhores filmes do gênero ficção científica"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogoAtualizadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Catálogo atualizado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogoDeletadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Catálogo deletado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogoLikeResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Like registrado com sucesso",
            data = new
            {
                totalLikes = 343,
                totalDislikes = 12
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class CatalogoDislikeResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Dislike registrado com sucesso",
            data = new
            {
                totalLikes = 342,
                totalDislikes = 13
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class NumeroFavoritosResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new
            {
                catalogoId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                numeroFavoritos = 89
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class TagsResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new List<string>
            {
                "Acao",
                "Aventura",
                "Animacao",
                "Comedia",
                "Drama",
                "Terror",
                "Suspense",
                "Romance",
                "FiccaoCientifica",
                "Fantasia",
                "Documentario",
                "Musical"
            },
            timestamp = DateTime.UtcNow
        };
    }

    // ========== ADMIN - RESPOSTAS ==========

    public class DashboardResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new DashboardStatusDTO
            {
                TotalFilmes = 150,
                TotalUsuarios = 342,
                FilmesPorGenero = new Dictionary<string, int>
                {
                    { "Ficção Científica", 45 },
                    { "Ação", 38 },
                    { "Drama", 32 },
                    { "Comédia", 35 }
                },
                FilmesPorAno = new Dictionary<string, int>
                {
                    { "2023", 12 },
                    { "2022", 18 },
                    { "2021", 15 },
                    { "2020", 20 }
                }
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class UsuariosListResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new ResultadoPaginaDTO<ListaUsuarioDTO>
            {
                Data = new List<ListaUsuarioDTO>
                {
                    new ListaUsuarioDTO
                    {
                        Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        Nome = "João Silva",
                        Email = "joao.silva@email.com",
                        Role = "User",
                        DataCriacao = DateTime.UtcNow.AddDays(-30)
                    },
                    new ListaUsuarioDTO
                    {
                        Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                        Nome = "Maria Santos",
                        Email = "maria.santos@email.com",
                        Role = "Admin",
                        DataCriacao = DateTime.UtcNow.AddDays(-60)
                    }
                },
                TotalItems = 342,
                PageNumber = 1,
                PageSize = 20
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class RoleAtualizadaResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Role do usuário atualizada com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    public class UsuarioDeletadoResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "Usuário deletado com sucesso",
            timestamp = DateTime.UtcNow
        };
    }

    public class FilmesStatsResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = true,
            httpCode = 200,
            message = "OK",
            data = new FilmesStatusDTO
            {
                TotalFilmes = 150,
                GeneroMaisComum = "Ficção Científica",
                AnoMaisRecente = "2023",
                AnoMaisAntigo = "1950"
            },
            timestamp = DateTime.UtcNow
        };
    }

    // ========== ERROS - RESPOSTAS ==========

    public class BadRequestResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = false,
            httpCode = 400,
            message = "Dados inválidos",
            errors = new List<string>
            {
                "O título é obrigatório",
                "O ano deve ser entre 1900 e 2100"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class NotFoundResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = false,
            httpCode = 404,
            message = "Recurso não encontrado",
            errors = new List<string>
            {
                "Filme com o ID especificado não foi encontrado"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class UnauthorizedResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = false,
            httpCode = 401,
            message = "Não autorizado",
            errors = new List<string>
            {
                "Token de autenticação inválido ou expirado"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class ForbiddenResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = false,
            httpCode = 403,
            message = "Acesso negado",
            errors = new List<string>
            {
                "Você não tem permissão para realizar esta ação"
            },
            timestamp = DateTime.UtcNow
        };
    }

    public class ConflictResponseExample : IExamplesProvider<object>
    {
        public object GetExamples() => new
        {
            succeeded = false,
            httpCode = 409,
            message = "Conflito",
            errors = new List<string>
            {
                "Já existe um usuário com este email"
            },
            timestamp = DateTime.UtcNow
        };
    }
}
