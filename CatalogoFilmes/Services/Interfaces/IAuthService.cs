using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using FluentResults;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDTO request);
    }
}
