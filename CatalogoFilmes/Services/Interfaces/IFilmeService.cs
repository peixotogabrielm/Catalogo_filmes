using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using FluentResults;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IFilmeService
    {
        Task<Result<ResultadoPaginaDTO<FilmeDTO>>> GetAllFilmes(FilmeFiltroDTO filter);
        Task<Result<FilmeDTO>> GetFilmeById(Guid id);
        Task<Result<FilmeDTO>> AddFilme(CriarFilmeDTO filme, Guid usuarioId);
        Task<Result<FilmeDTO>> UpdateFilme(FilmeUpdateDTO filme, Guid idFilme);
        Task<Result<bool>> DeleteFilme(Guid id);
    }
}
