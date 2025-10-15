using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegistrarAsync(RegistroDTO request);
        Task<ApiResponse<string>> LoginAsync(LoginDTO request);
        Task<ApiResponse<string>> RegistroAdmin(RegistroDTO request);
    }
}
