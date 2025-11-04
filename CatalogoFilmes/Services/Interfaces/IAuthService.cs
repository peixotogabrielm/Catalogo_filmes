using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDTO request);
        Task<Result<string>> RegistroAdmin(RegistroDTO request, string chaveSecreta);
    }
}
