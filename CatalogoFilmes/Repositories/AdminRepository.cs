using CatalogoFilmes.Data;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalFilmes()
        {
            return await _context.Filmes.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetTotalUsuarios()
        {
            return await _context.Usuarios.CountAsync().ConfigureAwait(false);
        }

        public async Task<Dictionary<string, int>> GetFilmesPorGenero()
        {
            return await _context.Filmes
                .Select(f => f.Genero)
                .GroupBy(g => g)
                .Select(g => new { Genero = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Genero, x => x.Count)
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<string, int>> GetFilmesPorAno()
        {
            return await _context.Filmes
                .GroupBy(f => f.Ano)
                .Select(g => new { Ano = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Ano)
                .Take(10)
                .ToDictionaryAsync(x => x.Ano, x => x.Count)
                .ConfigureAwait(false);
        }

        public async Task<(List<Usuario> usuarios, int totalCount)> GetUsuarios(int pageNumber, int pageSize)
        {
            var query = _context.Usuarios
            .AsQueryable();

            var totalCount = await query.CountAsync().ConfigureAwait(false);

            var usuarios = await query
                .OrderByDescending(u => u.DataCriacao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return (usuarios, totalCount);
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios
                .FindAsync(id);
        }

        public async Task<bool> UpdateUsuarioRole(Guid usuarioId, string novaRole)
        {
    

            var usuario = await _context.Usuarios.FindAsync(usuarioId).ConfigureAwait(false);

            if (usuario == null)
                return false;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> DeleteUsuario(Guid usuarioId)
        {
            

            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
            
        }

        public async Task<string?> GetGeneroMaisComum()
        {
            return await _context.Filmes
                .Select(f => f.Genero)
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<string?> GetAnoMaisRecente()
        {
            return await _context.Filmes
                .MaxAsync(f => f.Ano)
                .ConfigureAwait(false);
        }

        public async Task<string?> GetAnoMaisAntigo()
        {
            return await _context.Filmes
                .MinAsync(f => f.Ano)
                .ConfigureAwait(false);
        }

    }
}
