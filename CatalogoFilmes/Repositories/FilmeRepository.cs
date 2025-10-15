using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly AppDbContext _context;

        public FilmeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Filme> filmes, int totalCount)> GetAllFilmes(FilmeFiltroDTO filter)
        {
            var query = _context.Filmes.AsQueryable();

            // Aplicar filtros apenas se fornecidos
            if (!string.IsNullOrWhiteSpace(filter.Titulo))
            {
                query = query.Where(f => f.Titulo.ToLower().Contains(filter.Titulo.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Genero))
            {
                query = query.Where(f => f.Genero.ToLower().Contains(filter.Genero.ToLower()));
            }

            if (filter.Ano.HasValue)
            {
                query = query.Where(f => f.Ano == filter.Ano.Value);
            }

            // Contar total antes da paginação
            var totalCount = await query.CountAsync().ConfigureAwait(false);

            // Aplicar paginação e ordenação
            var filmes = await query
                .OrderByDescending(f => f.Ano)
                .ThenBy(f => f.Titulo)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return (filmes, totalCount);
        }
        public async Task<Filme> GetFilmeById(Guid id) { 
            var filme = await _context.Filmes.FindAsync(id).ConfigureAwait(false);
            return filme;
        }
        public async Task<Filme> AddFilme(Filme filme) {
            _context.Filmes.Add(filme); 
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return filme;
        }
        public async Task<Filme> UpdateFilme(Filme filme) { 
            _context.Filmes.Update(filme); 
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return filme;
        }
        public async Task<ApiResponse<bool>> DeleteFilme(Guid id)
        {
            var filme = await _context.Filmes.FindAsync(id).ConfigureAwait(false);

            if (filme == null)
            {
                return ApiResponse<bool>.Fail("Filme não encontrado");
            }

            try
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return ApiResponse<bool>.Ok(true);
            }
            catch (Exception)
            {
                return ApiResponse<bool>.Fail("Erro ao deletar filme");
            }
        }
    }
}
