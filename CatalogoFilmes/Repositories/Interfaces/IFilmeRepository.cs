using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repositories.Interfaces
{
    public interface IFilmeRepository
    {
        Task<(List<Filme> filmes, int totalCount)> GetAllFilmes(FilmeFiltroDTO filter);
        Task<Filme> GetFilmeById(Guid id);
        Task<Filme> AddFilme(Filme filme);
        Task<Filme> UpdateFilme(Filme filme);
        Task<Result<bool>> DeleteFilme(Guid id);
    }
}
