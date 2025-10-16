using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using CatalogoFilmes.Services.Interfaces;

namespace CatalogoFilmes.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<ApiResponse<FilmeDTO>> AddFilme(CriarFilmeDTO filme)
        {
            var novoFilme = new Filme
            {
                
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse
            };
            await _filmeRepository.AddFilme(novoFilme).ConfigureAwait(false);
            return ApiResponse<FilmeDTO>.Ok(new FilmeDTO
            {
                Id = novoFilme.Id,
                Titulo = novoFilme.Titulo,
                Genero = novoFilme.Genero,
                Ano = novoFilme.Ano,
                Sinopse = novoFilme.Sinopse
            });
        }


        public async Task<ApiResponse<bool>> DeleteFilme(Guid id)
        {
            var filmeExistente = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return ApiResponse<bool>.Fail("Filme não encontrado");
            }
            var retornoDelete = await _filmeRepository.DeleteFilme(id).ConfigureAwait(false);
            if(!retornoDelete.Sucesso)
            {
                return ApiResponse<bool>.Fail(retornoDelete.Mensagem);
            }
            return ApiResponse<bool>.Ok(true);

        }

        public async Task<ApiResponse<ResultadoPaginaDTO<FilmeDTO>>> GetAllFilmes(FilmeFiltroDTO filter)
        {
            try
            {
                var (filmes, totalCount) = await _filmeRepository.GetAllFilmes(filter).ConfigureAwait(false);

                var filmeDTOs = filmes.Select(f => new FilmeDTO
                {
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Genero = f.Genero,
                    Ano = f.Ano,
                    Sinopse = f.Sinopse
                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<FilmeDTO>
                {
                    Items = filmeDTOs,
                    TotalItems = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };

                return ApiResponse<ResultadoPaginaDTO<FilmeDTO>>.Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return ApiResponse<ResultadoPaginaDTO<FilmeDTO>>.Fail($"Erro ao buscar filmes: {ex.Message}");
            }
        }

        public async Task<ApiResponse<FilmeDTO>> GetFilmeById(Guid id)
        {
            
            if (id == Guid.Empty)
            {
                return ApiResponse<FilmeDTO>.Fail("ID do filme é obrigatório");
            }
            var filme = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filme == null)
            {
                return ApiResponse<FilmeDTO>.Fail("Filme não encontrado");
            }
            var filmeDTO = new FilmeDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse
            };
            return ApiResponse<FilmeDTO>.Ok(filmeDTO);

        }

        public async Task<ApiResponse<FilmeDTO>> UpdateFilme(FilmeUpdateDTO filme)
        {

            var filmeExistente = await _filmeRepository.GetFilmeById(filme.Id).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return ApiResponse<FilmeDTO>.Fail("Filme não encontrado");
            }

            filmeExistente.Titulo = filme.Titulo;
            filmeExistente.Genero = filme.Genero;
            filmeExistente.Ano = filme.Ano;
            filmeExistente.Sinopse = filme.Sinopse;
            await _filmeRepository.UpdateFilme(filmeExistente).ConfigureAwait(false);
            return ApiResponse<FilmeDTO>.Ok(new FilmeDTO
            {
                Id = filmeExistente.Id,
                Titulo = filmeExistente.Titulo,
                Genero = filmeExistente.Genero,
                Ano = filmeExistente.Ano,
                Sinopse = filmeExistente.Sinopse
            });
        }
    }
}
