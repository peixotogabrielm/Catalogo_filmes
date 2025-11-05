using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Result<DashboardStatusDTO>> GetDashboardStats();
        Task<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>> GetUsuarios(int pageNumber = 1, int pageSize = 20);
        Task<Result<string>> UpdateUserRole(UpdateRoleDTO dto);
        Task<Result<string>> DeleteUsuario(Guid usuarioId, Guid currentUserId);
        Task<Result<FilmesStatusDTO>> GetFilmesStats();
    }
}
