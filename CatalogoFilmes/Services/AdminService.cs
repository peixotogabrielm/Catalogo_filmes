using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories;
using CatalogoFilmes.Repositories.Interfaces;
using CatalogoFilmes.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static CatalogoFilmes.Helpers.Errors;
using static CatalogoFilmes.Helpers.Successes;

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
    
                  return Result.Ok()
                     .WithSuccess(new OkSuccess(stats));
            }
            catch (Exception ex)
            {
                return Result.Fail<DashboardStatusDTO>(new BadRequestError($"Erro ao obter estatísticas: {ex.Message}"));
            }
        }

        public async Task<Result<ResultadoPaginaDTO<ListaUsuarioDTO>>> GetUsuarios(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var (usuarios, totalCount) = await _adminRepository.GetUsuarios(pageNumber, pageSize).ConfigureAwait(false);
                if(usuarios.Count == 0 && totalCount == 0)
                {
                    var retornoVazio = new ResultadoPaginaDTO<ListaUsuarioDTO>
                    {
                        Data = new List<ListaUsuarioDTO>(),
                        TotalItems = 0,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    };

                    return Result.Ok()
                        .WithSuccess(new OkSuccess(retornoVazio));
                }
                var usuarioDTOs = usuarios.Select(u => new ListaUsuarioDTO
                {
                    Nome = u.Nome,
                    Email = u.Email,
                    DataCriacao = u.DataCriacao,
                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<ListaUsuarioDTO>
                {
                    Data = usuarioDTOs,
                    TotalItems = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Result.Ok()
                    .WithSuccess(new OkSuccess(pagedResult));
            }
            catch (Exception ex)
            {
                return Result.Fail<ResultadoPaginaDTO<ListaUsuarioDTO>>(new BadRequestError($"Erro ao listar usuários: {ex.Message}"));
            }
        }

        public async Task<Result<string>> UpdateUserRole(UpdateRoleDTO dto)
        {
            try
            {
                if (dto.NovaRole != "Admin" && dto.NovaRole != "User")
                {
                    return Result.Fail<string>(new BadRequestError("Role inválida. Use 'Admin' ou 'User'."));
                }

                var sucesso = await _adminRepository.UpdateUsuarioRole(dto.UsuarioId, dto.NovaRole).ConfigureAwait(false);

                if (!sucesso)
                {
                    return Result.Fail<string>(new NotFoundError("Usuário não encontrado."));
                }

                return Result.Ok().WithSuccess(new OkSuccess($"Role do usuário atualizada para {dto.NovaRole}"));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(new BadRequestError($"Erro ao atualizar role: {ex.Message}"));
            }
        }

        public async Task<Result<string>> DeleteUsuario(Guid usuarioId, Guid currentUserId)
        {
            try
            {
                if (currentUserId == usuarioId)
                {
                    return Result.Fail<string>(new BadRequestError("Você não pode deletar sua própria conta."));
                }

                var sucesso = await _adminRepository.DeleteUsuario(usuarioId).ConfigureAwait(false);

                if (!sucesso)
                {
                    return Result.Fail<string>(new NotFoundError("Usuário não encontrado."));
                }

                return Result.Ok().WithSuccess(new OkSuccess("Usuário deletado com sucesso."));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(new BadRequestError($"Erro ao deletar usuário: {ex.Message}"));
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

                return Result.Ok()
                    .WithSuccess(new OkSuccess(stats));
            }
            catch (Exception ex)
            {
                return Result.Fail<FilmesStatusDTO>(new BadRequestError($"Erro ao obter estatísticas de filmes: {ex.Message}"));
            }
        }
    }
}
