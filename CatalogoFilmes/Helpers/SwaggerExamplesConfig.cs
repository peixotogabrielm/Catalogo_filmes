using CatalogoFilmes.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CatalogoFilmes.Helpers
{
    // status: 200
    public class ResultSuccessFilmeDTOExample : IExamplesProvider<Result<FilmeDTO>>
    {
        public Result<FilmeDTO> GetExamples() =>
         new Result<FilmeDTO>
         {
             Sucesso = true,
             StatusCode = 200,
             Data = new FilmeDTO
             {
                 Id = Guid.NewGuid(),
                 Titulo = "Inception",
                 Generos = "Sci-Fi" ,
                 Ano = "2010",
                 Sinopse = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O."
             }
         };
    }
    public class ResultSuccessPaginaFilmeDTOExample : IExamplesProvider<Result<ResultadoPaginaDTO<FilmeDTO>>>
    {
        public Result<ResultadoPaginaDTO<FilmeDTO>> GetExamples() =>
         new Result<ResultadoPaginaDTO<FilmeDTO>>
         {
             Sucesso = true,
             StatusCode = 200,
             Data = new ResultadoPaginaDTO<FilmeDTO>
             {
                 Data = new List<FilmeDTO>
                {
                    new FilmeDTO
                    {
                    Id = Guid.NewGuid(),
                    Titulo = "Inception",
                    Generos = "Sci-Fi",
                    Ano = "2010",
                    Sinopse = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O."
                    },
                    new FilmeDTO
                    {
                    Id = Guid.NewGuid(),
                    Titulo = "The Dark Knight",
                    Generos = "Action" ,
                    Ano = "2008",
                    Sinopse = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice."
                    }
                },
                 PageNumber = 1,
                 PageSize = 5,
                 TotalItems = 10
             },
         };
    }
    public class ResultSuccessStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string> GetExamples() =>
          new Result<string>
          {
              StatusCode = 200,
              Sucesso = true,
              Data = "Operação realizada com sucesso."
          };
    }
    public class ResultSuccessBoolExample : IExamplesProvider<Result<bool>>
    {
        public Result<bool> GetExamples() =>
            new Result<bool>
            {
                StatusCode = 200,
                Sucesso = true,
                Data = true
            };
    }
    public class ResultSuccessDashboardStatusDTOExample : IExamplesProvider<DashboardStatusDTO>
    {
        public DashboardStatusDTO GetExamples() =>
            new DashboardStatusDTO
            {
                TotalFilmes = 150,
                TotalUsuarios = 5000,
                FilmesPorGenero = new Dictionary<string, int>
                {
                    { "Action", 40 },
                    { "Comedy", 30 },
                    { "Drama", 50 },
                    { "Sci-Fi", 30 }
                },
                FilmesPorAno = new Dictionary<string, int>
                {
                    { "2020", 20 },
                    { "2019", 25 },
                    { "2018", 30 },
                    { "2017", 35 }
                }
            };
    }
    public class ResultSuccessResultadoPaginaUsuarioDTOExample : IExamplesProvider<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>>
    {
    public Result<ResultadoPaginaDTO<ListaUsuarioDTO>> GetExamples() =>
            new Result<ResultadoPaginaDTO<ListaUsuarioDTO>>
            {
                Sucesso = true,
                StatusCode = 200,
                Data = new ResultadoPaginaDTO<ListaUsuarioDTO>
                {
                    Data = new List<ListaUsuarioDTO>
                {
                    new ListaUsuarioDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = "João Silva",
                        Email = "JoaoSilva@gmail.com",
                        Role = "User",
                        DataCriacao = DateTime.UtcNow.AddMonths(-3)
                    },
                    new ListaUsuarioDTO
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Maria Oliveira",
                        Email = "MariaOli@hotmail.com",
                        Role = "Admin",
                        DataCriacao = DateTime.UtcNow.AddMonths(-5)
                    }

                    },
                    PageNumber = 1,
                    PageSize = 2,
                    TotalItems = 50
                }
            };
    }
    public class ResultSuccessFilmesStatusDTOExample : IExamplesProvider<Result<FilmesStatusDTO>>
    {
        public Result<FilmesStatusDTO> GetExamples() =>
         new Result<FilmesStatusDTO>
         {
             Sucesso = true,
             StatusCode = 200,
             Data = new FilmesStatusDTO
             {
                 TotalFilmes = 150,
                 GeneroMaisComum = "Drama",
                 AnoMaisRecente = "2023",
                 AnoMaisAntigo = "1980"
             }
         };
    }

    // status: 201
    public class ResultCreatedStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string> GetExamples() =>
           new Result<string>
           {
               StatusCode = 201,
               Sucesso = true,
               Data = "Dados criados com sucesso"
           };
    }
    public class ResultCreatedFilmeDTOExample : IExamplesProvider<Result<FilmeDTO>>
    {
        public Result<FilmeDTO> GetExamples() =>
         new Result<FilmeDTO>
         {
             Sucesso = true,
             StatusCode = 201,
             Data = new FilmeDTO
             {
                 Id = Guid.NewGuid(),
                 Titulo = "Inception",
                 Generos = "Sci-Fi",
                 Ano = "2010",
                 Sinopse = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O."
             }
         };
    }

    // status: 204
    public class ResultNoContentStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string> GetExamples() =>
            new Result<string>
            {
                StatusCode = 204,
                Sucesso = true,
                Data = ""
            };
    }
    public class ResultNoContentPaginaUsuarioDTOExample : IExamplesProvider<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>>
    {
        public Result<ResultadoPaginaDTO<ListaUsuarioDTO>> GetExamples() =>
            new Result<ResultadoPaginaDTO<ListaUsuarioDTO>>
            {
                StatusCode = 204,
                Sucesso = true,
                Data = new ResultadoPaginaDTO<ListaUsuarioDTO>()
                {
                    Data = new List<ListaUsuarioDTO>(),
                    PageNumber = 1,
                    PageSize = 10,
                    TotalItems = 0
                }
            };
    }
    public class ResultNoContentPaginaFilmeDTOExample : IExamplesProvider<Result<ResultadoPaginaDTO<FilmeDTO>>>
    {
        public Result<ResultadoPaginaDTO<FilmeDTO>> GetExamples() =>
            new Result<ResultadoPaginaDTO<FilmeDTO>>
            {
                StatusCode = 204,
                Sucesso = true,
                Data = new ResultadoPaginaDTO<FilmeDTO>
                {
                    Data = new List<FilmeDTO>(),
                    PageNumber = 1,
                    PageSize = 10,
                    TotalItems = 0
                }
            };
    }

    // status: 400
    public class ResultBadRequestPaginaFilmeDTOExample : IExamplesProvider<Result<ResultadoPaginaDTO<FilmeDTO>>>
    {
        public Result<ResultadoPaginaDTO<FilmeDTO>> GetExamples() =>
            new Result<ResultadoPaginaDTO<FilmeDTO>>
            {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Parâmetros inválidos."
            };
    }
    public class ResultBadRequestStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string>GetExamples() =>
            new Result<string>
            {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao processar a solicitação."
            };
    }
    public class ResultBadRequestBoolExample : IExamplesProvider<Result<bool>>
    {
        public Result<bool> GetExamples() =>
            new Result<bool>
            {
                StatusCode = 400,
                Sucesso = false,
            };
    }
    public class ResultBadRequestDashboardStatusDTOExample :
        IExamplesProvider<Result<DashboardStatusDTO>>
    {
        public Result<DashboardStatusDTO> GetExamples() =>
              new Result<DashboardStatusDTO>
              {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao obter as estatísticas do dashboard."
              };
    }
    public class ResultBadRequestFilmeStatusDTOExample : IExamplesProvider<Result<FilmesStatusDTO>>
    {
        public Result<FilmesStatusDTO> GetExamples() =>
            new Result<FilmesStatusDTO>
            {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao obter as estatísticas dos filmes."
            };
    }
    public class ResultBadRequestPaginaListaUsuarioDTOExample : 
            
            IExamplesProvider<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>>
        {
        public Result<ResultadoPaginaDTO<ListaUsuarioDTO>> GetExamples() =>
            new Result<ResultadoPaginaDTO<ListaUsuarioDTO>>
            {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao obter a lista de usuários."
            };
    }
    public class ResultBadRequestFilmeDTOExample : IExamplesProvider<Result<FilmeDTO>>
    {
        public Result<FilmeDTO> GetExamples() =>
            new Result<FilmeDTO>
            {
                StatusCode = 400,
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao processar o filme."
            };
    }
   
    // status: 401
    public class ResultUnauthorizedStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string> GetExamples() =>
            new Result<string>
            {
                StatusCode = 401,
                Sucesso = false,
                Mensagem = "Não autorizado. Credenciais inválidas."
            };
    }

    // status: 404
    public class ResultNotFoundStringExample : IExamplesProvider<Result<string>>
    {
        public Result<string> GetExamples() =>
            new Result<string>
            {
                StatusCode = 404,
                Sucesso = false,
                Mensagem = "Recurso não encontrado."
            };
    }
    public class ResultNotFoundFilmeDTOExample : IExamplesProvider<Result<FilmeDTO>>
    {
        public Result<FilmeDTO> GetExamples() =>
            new Result<FilmeDTO>
            {
                StatusCode = 404,
                Sucesso = false,
                Mensagem = "Filme não encontrado."
            };
    }
    public class ResultNotFoundBoolExample : IExamplesProvider<Result<bool>>
    {
        public Result<bool> GetExamples() =>
            new Result<bool>
            {
                StatusCode = 404,
                Sucesso = false,
                Mensagem = "Recurso não encontrado."
            };
    }

}
