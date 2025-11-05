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

            var totalCount = await query.CountAsync().ConfigureAwait(false);

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
            var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);
            
            return filme;
        }
        public async Task<Filme> AddFilme(Filme filme) {
            var existingFilme = await _context.Filmes
                .FirstOrDefaultAsync(f => f.Titulo.ToLower() == filme.Titulo.ToLower() && f.Ano == filme.Ano)
                .ConfigureAwait(false);
            if (existingFilme is null)
            {
                _context.Filmes.Add(filme); 
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return filme;

            }
            else
            {
                return null;
            }
        }
        public async Task<Filme> UpdateFilme(Filme filme) { 
            var existingFilmeNome = await _context.Filmes
                .FirstOrDefaultAsync(f => f.Titulo.ToLower() == filme.Titulo.ToLower() && f.Ano == filme.Ano && f.Id != filme.Id)
                .ConfigureAwait(false);
            if (existingFilmeNome is null)
            {
                _context.Filmes.Update(filme); 
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return filme;

            }
            else
            {
                return null;
            }

        }
        public async Task<Result<bool>> DeleteFilme(Guid id)
        {
            var filme = await _context.Filmes.FindAsync(id).ConfigureAwait(false);

            if (filme == null)
            {
                return Result<bool>.Fail(400, "Filme não encontrado");
            }

            try
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return Result<bool>.Ok(200, true);
            }
            catch (Exception)
            {
                return Result<bool>.Fail(400,"Erro ao deletar filme");
            }
        }
    }
}
