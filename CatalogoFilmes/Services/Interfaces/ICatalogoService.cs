using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;
using FluentResults;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface ICatalogoService
    {
        Task<Result<IEnumerable<CatalogoDTO>>> GetAllCatalogosAsync(FilterCatalogoDTO filtroDto);
        Task<Result<IEnumerable<CatalogoDTO>>> GetCatalogosByIdAsync(string id);
        Task<Result> AddUserCatalogoAsync(CriarCatalogoDTO criaCatalogoDto, string? usuarioId);
        Task<Result> UpdateUserCatalogoAsync(CatalogoDTO catalogoDto, string usuarioId);
        Task <Result>DeleteUserCatalogoAsync(string id, string usuarioId);
        Task <Result>LikeCatalogoAsync(string catalogoId);
        Task<Result> DislikeCatalogoAsync(string catalogoId);
        Task <Result>FavoritarCatalogoAsync(string catalogoId);
        Task <Result>RemoverFavoritoCatalogoAsync(string catalogoId);
        Task<Result<int?>> GetNumeroFavoritosAsync(string catalogoId);
        Task<Result<IEnumerable<Tags>>> GetAllTagsAsync();

    }
}