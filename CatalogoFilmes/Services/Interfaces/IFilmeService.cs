using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IFilmeService
    {
        Task<ApiResponse<ResultadoPaginaDTO<FilmeDTO>>> GetAllFilmes(FilmeFiltroDTO filter);
        Task<ApiResponse<FilmeDTO>> GetFilmeById(Guid id);
        Task<ApiResponse<FilmeDTO>> AddFilme(CriarFilmeDTO filme);
        Task<ApiResponse<FilmeDTO>> UpdateFilme(FilmeUpdateDTO filme);
        Task<ApiResponse<bool>> DeleteFilme(Guid id);
    }
}
