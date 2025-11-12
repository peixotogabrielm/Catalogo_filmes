using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CatalogoFilmes.Helpers.Errors;

namespace CatalogoFilmes.Repositories
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly AppDbContext _context;
        public CatalogoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCatalogoAsync(Catalogo catalogo)
        {
            var addCatalogo = await _context.Catalogos.AddAsync(catalogo).ConfigureAwait(false);
            if (addCatalogo != null)
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            
        }

        public async Task DeleteCatalogoAsync(string id)
        {
            var deletarCatalogo = await _context.Catalogos.FindAsync(id).ConfigureAwait(false);
            if (deletarCatalogo != null)
            {
                _context.Catalogos.Remove(deletarCatalogo);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DislikeCatalogoAsync(string catalogoId)
        {
            try
            {
                var catalogo = await _context.Catalogos.FindAsync(catalogoId).ConfigureAwait(false);
                if (catalogo != null)
                {
                    catalogo.Dislikes += 1;
                    _context.Catalogos.Update(catalogo);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                
            }catch(Exception ex)
            {
            
                throw new Exception(new BadRequestError("Erro ao descurtir catálogo.").ToString(), ex);
            }
        }

        public async Task<IEnumerable<Catalogo>> GetAllCatalogosByUserId()
        {
            var catalogos = await _context.Catalogos.ToListAsync().ConfigureAwait(false);
            return catalogos;
        }

        public async Task<IEnumerable<Catalogo>> GetAllCatalogosByUserIdAsync(string id)
        {
           
                var catalogos = await _context.Catalogos.Where(c => c.UsuarioId == id).ToListAsync().ConfigureAwait(false);
                return catalogos;
        }

        public async Task<IEnumerable<Catalogo>> GetAllCatalogos(FilterCatalogoDTO filtroDto)
        {
            try
            {
                 var query = _context.Catalogos.AsQueryable();

                if (!string.IsNullOrEmpty(filtroDto.nomeCatalogo))
                    query = query.Where(c => c.Nome.Contains(filtroDto.nomeCatalogo));

                if (filtroDto.tagCatalogo.HasValue)
                    query = query.Where(c => (c.Tags & filtroDto.tagCatalogo.Value) == filtroDto.tagCatalogo.Value);

                if (filtroDto.minLikes.HasValue)
                    query = query.Where(c => c.Likes >= filtroDto.minLikes.Value);

                if (filtroDto.minFavoritos.HasValue)
                    query = query.Where(c => c.NumeroFavoritos >= filtroDto.minFavoritos.Value);

                if (!string.IsNullOrEmpty(filtroDto.nomeUsuario))
                    query = query.Include(c => c.Usuario)
                                .Where(c => c.Usuario.Nome.Contains(filtroDto.nomeUsuario));

                var catalogos = await query.ToListAsync().ConfigureAwait(false);
                return catalogos;
            }
            catch (Exception ex)
            {
                throw new Exception(new BadRequestError("Erro ao carregar catálogos.").ToString(), ex);
            }
            
        }

        public async Task<IEnumerable<Catalogo>> GetCatalogosMaisFavoritadosAsync()
        {
            var catalogos = await _context.Catalogos
                .OrderByDescending(c => c.NumeroFavoritos)
                .Take(10)
                .ToListAsync().ConfigureAwait(false);
            return catalogos;
        }

        public async Task<IEnumerable<Catalogo>> GetCatalogosMaisLikadosAsync()
        {
            var catalogos = await _context.Catalogos
                .OrderByDescending(c => c.Likes)
                .Take(10)
                .ToListAsync().ConfigureAwait(false);
            return catalogos;
        }

        public async Task<int?> GetNumeroFavoritosAsync(string catalogoId)
        {
            
            var catalogo = await _context.Catalogos.FindAsync(catalogoId).ConfigureAwait(false);
            if (catalogo != null)
            {
                return catalogo.NumeroFavoritos;
            }
            return null;
        }

        public async Task LikeCatalogoAsync(string catalogoId)
        {
            try
            {
                var catalogo = await _context.Catalogos.FindAsync(catalogoId).ConfigureAwait(false);
                if (catalogo != null)
                {
                    catalogo.Likes += 1;
                    _context.Catalogos.Update(catalogo);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(new BadRequestError("Erro ao curtir catálogo.").ToString(), ex);
            }
        }

        public async Task UpdateCatalogoAsync(Catalogo catalogo)
        {
            _context.Catalogos.Update(catalogo);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoverFavoritoCatalogoAsync(string catalogoId)
        {
            throw new NotImplementedException();
        }

        public async Task FavoritarCatalogoAsync(string catalogoId)
        {
            throw new NotImplementedException();
        }
        
    }
}