using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<DashboardStatusDTO>> GetDashboardStats();
        Task<ApiResponse<ResultadoPaginaDTO<ListaUsuarioDTO>>> GetUsuarios(int pageNumber = 1, int pageSize = 20);
        Task<ApiResponse<string>> UpdateUserRole(UpdateRoleDTO dto);
        Task<ApiResponse<string>> DeleteUsuario(Guid usuarioId, string currentUserId);
        Task<ApiResponse<FilmesStatusDTO>> GetFilmesStats();
    }
}
