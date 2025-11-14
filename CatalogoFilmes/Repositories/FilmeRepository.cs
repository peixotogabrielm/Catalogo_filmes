using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
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
            try
            {
                
                var query = _context.Filmes.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter.Titulo))
                {
                    query = query.Where(f => f.Titulo.ToLower().Contains(filter.Titulo.ToLower()));
                }

                if (filter.Genero != null && filter.Genero.Count > 0)
                {
                    query = query.Where(f => f.Genero.Contains(f.Genero));
                }
            
                if (!string.IsNullOrWhiteSpace(filter.Ano))
                {
                    query = query.Where(f => f.Ano == filter.Ano);
                }

                var totalCount = await query.CountAsync().ConfigureAwait(false);


                // buscar id da pagina
                var skip = (filter.PageNumber - 1) * filter.PageSize;

                var idsPage = await query
                .OrderBy(f => f.Titulo) 
                .Select(f => f.Id)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

                // buscar filmes completos com os ids da pagina
                var filmesPage = await _context.Filmes
                .AsNoTracking()
                .Where(f => idsPage.Contains(f.Id))
                .Select(f => new Filme {
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Ano = f.Ano,
                    Genero = f.Genero,
                    Sinopse = f.Sinopse,
                    Duracao = f.Duracao,
                    Idioma = f.Idioma,
                    Poster = f.Poster,
                    Trailer = f.Trailer
                })
                .ToListAsync();

                // buscar equipes tecnicas dos filmes da pagina
                var equipes = await _context.EquipeTecnicas
                .AsNoTracking()
                .Where(e => idsPage.Contains(e.FilmeId))
                .Select(e => new EquipeTecnica {
                    Id = e.Id,
                    FilmeId = e.FilmeId,
                    Nome = e.Nome,
                    Cargo = e.Cargo
                })
                .ToListAsync();

                // buscar notas externas dos filmes da pagina
                var classificacoes = await _context.Classificacoes
                .AsNoTracking()
                .Where(c => idsPage.Contains(c.FilmeId))
                .Select(c => new Classificacoes {
                    Id = c.Id,
                    FilmeId = c.FilmeId,
                    Fonte = c.Fonte,
                    Nota = c.Nota
                })
                .ToListAsync();

                // associar equipes tecnicas e notas externas aos filmes
                foreach (var filme in filmesPage)
                {
                    filme.Equipes = equipes.Where(e => e.FilmeId == filme.Id).ToList();
                    filme.Classificacoes = classificacoes.Where(c => c.FilmeId == filme.Id).ToList();
                }

    
                return (filmesPage, totalCount);
            }catch(SqlException ex)
            {
                throw new Exception("Erro ao buscar filmes: " + ex.Message);
            }
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
        public async Task<bool> DeleteFilme(Guid id)
        {
            var filme = await _context.Filmes.FindAsync(id).ConfigureAwait(false);

            if (filme == null)
            {
                return false;
            }

            try
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
