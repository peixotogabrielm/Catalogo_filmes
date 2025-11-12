using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repositories.Interfaces
{
    public interface ICatalogoRepository
    {
        Task<IEnumerable<Catalogo>> GetAllCatalogos(FilterCatalogoDTO filtroDto);
        Task<IEnumerable<Catalogo>> GetAllCatalogosByUserIdAsync(string id);
        Task<IEnumerable<Catalogo>> GetCatalogosMaisLikadosAsync();
        Task<IEnumerable<Catalogo>> GetCatalogosMaisFavoritadosAsync();
        Task AddCatalogoAsync(Catalogo catalogo);
        Task UpdateCatalogoAsync(Catalogo catalogo);
        Task DeleteCatalogoAsync(string id);
        Task LikeCatalogoAsync(string catalogoId);
        Task DislikeCatalogoAsync(string catalogoId);
        Task FavoritarCatalogoAsync(string catalogoId);
        Task RemoverFavoritoCatalogoAsync(string catalogoId);
        Task<int?> GetNumeroFavoritosAsync(string catalogoId);
    }
}