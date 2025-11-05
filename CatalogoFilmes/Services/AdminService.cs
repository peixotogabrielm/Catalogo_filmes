using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
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

        public async Task<Result<DashboardStatusDTO>> GetDashboardStats()
        {
            try
            {
                var totalFilmes = await _adminRepository.GetTotalFilmes().ConfigureAwait(false);
                var totalUsuarios = await _adminRepository.GetTotalUsuarios().ConfigureAwait(false);
                var filmesPorGenero = await _adminRepository.GetFilmesPorGenero().ConfigureAwait(false);
                var filmesPorAno = await _adminRepository.GetFilmesPorAno().ConfigureAwait(false);

                var stats = new DashboardStatusDTO
                {
                    TotalFilmes = totalFilmes,
                    TotalUsuarios = totalUsuarios,
                    FilmesPorGenero = filmesPorGenero,
                    FilmesPorAno = filmesPorAno,
                };

                return Result<DashboardStatusDTO>.Ok(200, stats);
            }
            catch (Exception ex)
            {
                return Result<DashboardStatusDTO>.Fail(400, $"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        public async Task<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>> GetUsuarios(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var (usuarios, totalCount) = await _adminRepository.GetUsuarios(pageNumber, pageSize).ConfigureAwait(false);
                if(usuarios.Count == 0 && totalCount == 0)
                {
                    return Result<ResultadoPaginaDTO<ListaUsuarioDTO>>.Ok(204, new ResultadoPaginaDTO<ListaUsuarioDTO>
                    {
                        Items = new List<ListaUsuarioDTO>(),
                        TotalItems = 0,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });
                }
                var usuarioDTOs = usuarios.Select(u => new ListaUsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Role = u.Role,
                    DataCriacao = u.DataCriacao,
                    FilmesAdicionados = u.FilmesCriados.Count()
                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<ListaUsuarioDTO>
                {
                    Items = usuarioDTOs,
                    TotalItems = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Result<ResultadoPaginaDTO<ListaUsuarioDTO>>.Ok(200, pagedResult);
            }
            catch (Exception ex)
            {
                return Result<ResultadoPaginaDTO<ListaUsuarioDTO>>.Fail(400, $"Erro ao listar usuários: {ex.Message}");
            }
        }

        public async Task<Result<string>> UpdateUserRole(UpdateRoleDTO dto)
        {
            try
            {
                if (dto.NovaRole != "Admin" && dto.NovaRole != "User")
                {
                    return Result<string>.Fail(401, "Role inválida. Use 'Admin' ou 'User'.");
                }

                var sucesso = await _adminRepository.UpdateUsuarioRole(dto.UsuarioId, dto.NovaRole).ConfigureAwait(false);

                if (!sucesso)
                {
                    return Result<string>.Fail(404, "Usuário não encontrado.");
                }

                return Result<string>.Ok(200, $"Role do usuário atualizada para {dto.NovaRole}");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(400, $"Erro ao atualizar role: {ex.Message}");
            }
        }

        public async Task<Result<string>> DeleteUsuario(Guid usuarioId, Guid currentUserId)
        {
            try
            {
                if (currentUserId == usuarioId)
                {
                    return Result<string>.Fail(401, "Você não pode deletar sua própria conta.");
                }

                var sucesso = await _adminRepository.DeleteUsuario(usuarioId).ConfigureAwait(false);

                if (!sucesso)
                {
                    return Result<string>.Fail(404, "Usuário não encontrado.");
                }

                return Result<string>.Ok(200, "Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(400, $"Erro ao deletar usuário: {ex.Message}");
            }
        }

        public async Task<Result<FilmesStatusDTO>> GetFilmesStats()
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
                    AnoMaisAntigo = anoMaisAntigo,
                };

                return Result<FilmesStatusDTO>.Ok(200, stats);
            }
            catch (Exception ex)
            {
                return Result<FilmesStatusDTO>.Fail(400, $"Erro ao obter estatísticas de filmes: {ex.Message}");
            }
        }
    }
}
