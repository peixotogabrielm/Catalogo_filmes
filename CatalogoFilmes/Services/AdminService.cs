using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Repositories;
using CatalogoFilmes.Repositories.Interfaces;
using CatalogoFilmes.Services.Interfaces;

namespace CatalogoFilmes.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<ApiResponse<DashboardStatusDTO>> GetDashboardStats()
        {
            try
            {
                var totalFilmes = await _adminRepository.GetTotalFilmes().ConfigureAwait(false);
                var totalUsuarios = await _adminRepository.GetTotalUsuarios().ConfigureAwait(false);
                var totalAdmins = await _adminRepository.GetTotalAdmins().ConfigureAwait(false);
                var filmesPorGenero = await _adminRepository.GetFilmesPorGenero().ConfigureAwait(false);
                var filmesPorAno = await _adminRepository.GetFilmesPorAno().ConfigureAwait(false);

                var stats = new DashboardStatusDTO
                {
                    TotalFilmes = totalFilmes,
                    TotalUsuarios = totalUsuarios,
                    TotalAdmins = totalAdmins,
                    FilmesPorGenero = filmesPorGenero,
                    FilmesPorAno = filmesPorAno
                };

                return ApiResponse<DashboardStatusDTO>.Ok(stats);
            }
            catch (Exception ex)
            {
                return ApiResponse<DashboardStatusDTO>.Fail($"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ResultadoPaginaDTO<ListaUsuarioDTO>>> GetUsuarios(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var (usuarios, totalCount) = await _adminRepository.GetUsuarios(pageNumber, pageSize).ConfigureAwait(false);

                var usuarioDTOs = usuarios.Select(u => new ListaUsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Role = u.Role,
                    DataCriacao = u.DataCriacao
                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<ListaUsuarioDTO>
                {
                    Items = usuarioDTOs,
                    TotalItems = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return ApiResponse<ResultadoPaginaDTO<ListaUsuarioDTO>>.Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return ApiResponse<ResultadoPaginaDTO<ListaUsuarioDTO>>.Fail($"Erro ao listar usuários: {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> UpdateUserRole(UpdateRoleDTO dto)
        {
            try
            {
                if (dto.NovaRole != "Admin" && dto.NovaRole != "User")
                {
                    return ApiResponse<string>.Fail("Role inválida. Use 'Admin' ou 'User'.");
                }

                var sucesso = await _adminRepository.UpdateUsuarioRole(dto.UsuarioId, dto.NovaRole).ConfigureAwait(false);

                if (!sucesso)
                {
                    return ApiResponse<string>.Fail("Usuário não encontrado.");
                }

                return ApiResponse<string>.Ok($"Role do usuário atualizada para {dto.NovaRole}");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail($"Erro ao atualizar role: {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> DeleteUsuario(Guid usuarioId, string currentUserId)
        {
            try
            {
                if (currentUserId == usuarioId.ToString())
                {
                    return ApiResponse<string>.Fail("Você não pode deletar sua própria conta.");
                }

                var sucesso = await _adminRepository.DeleteUsuario(usuarioId).ConfigureAwait(false);

                if (!sucesso)
                {
                    return ApiResponse<string>.Fail("Usuário não encontrado.");
                }

                return ApiResponse<string>.Ok("Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail($"Erro ao deletar usuário: {ex.Message}");
            }
        }

        public async Task<ApiResponse<FilmesStatusDTO>> GetFilmesStats()
        {
            try
            {
                var totalFilmes = await _adminRepository.GetTotalFilmes().ConfigureAwait(false);
                var generoMaisComum = await _adminRepository.GetGeneroMaisComum().ConfigureAwait(false);
                var anoMaisRecente = await _adminRepository.GetAnoMaisRecente().ConfigureAwait(false);
                var anoMaisAntigo = await _adminRepository.GetAnoMaisAntigo().ConfigureAwait(false);

                var stats = new FilmesStatusDTO
                {
                    TotalFilmes = totalFilmes,
                    GeneroMaisComum = generoMaisComum ?? "N/A",
                    AnoMaisRecente = anoMaisRecente,
                    AnoMaisAntigo = anoMaisAntigo
                };

                return ApiResponse<FilmesStatusDTO>.Ok(stats);
            }
            catch (Exception ex)
            {
                return ApiResponse<FilmesStatusDTO>.Fail($"Erro ao obter estatísticas de filmes: {ex.Message}");
            }
        }
    }
}
