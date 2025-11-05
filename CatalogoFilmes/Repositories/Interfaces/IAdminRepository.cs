using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<int> GetTotalFilmes();
        Task<int> GetTotalUsuarios();
        Task<Dictionary<string, int>> GetFilmesPorGenero();
        Task<Dictionary<int, int>> GetFilmesPorAno();
        Task<(List<Usuario> usuarios, int totalCount)> GetUsuarios(int pageNumber, int pageSize);
        Task<Usuario?> GetUsuarioById(Guid id);
        Task<bool> UpdateUsuarioRole(Guid usuarioId, string novaRole);
        Task<bool> DeleteUsuario(Guid usuarioId);
        Task<String?> GetGeneroMaisComum();
        Task<int?> GetAnoMaisRecente();
        Task<int?> GetAnoMaisAntigo();

    }
}
