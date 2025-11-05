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

        public async Task<Result<FilmeDTO>> AddFilme(CriarFilmeDTO filme, Guid usuarioId)
        {
            var novoFilme = new Filme
            {

                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse,
                Duracao = filme.Duracao
            };
            var retornoFilme = await _filmeRepository.AddFilme(novoFilme).ConfigureAwait(false);
            if(retornoFilme == null)
            {
                return Result<FilmeDTO>.Fail(400, "Filme já existe");
            }
            return Result<FilmeDTO>.Ok(201, new FilmeDTO
            {
                Id = retornoFilme.Id,
                Titulo = retornoFilme.Titulo,
                Genero = retornoFilme.Genero,
                Ano = retornoFilme.Ano,
                Sinopse = retornoFilme.Sinopse,
                Duracao = retornoFilme.Duracao,
                DataAdicionado = retornoFilme.DataCriacao
            });
        }


        public async Task<Result<bool>> DeleteFilme(Guid id)
        {
            var filmeExistente = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return Result<bool>.Fail(404, "Filme não encontrado");
            }
            var retornoDelete = await _filmeRepository.DeleteFilme(id).ConfigureAwait(false);
            if(!retornoDelete.Sucesso)
            {
                return Result<bool>.Fail(400, retornoDelete.Mensagem);
            }
            return Result<bool>.Ok(200, true);

        }

        public async Task<Result<ResultadoPaginaDTO<FilmeDTO>>> GetAllFilmes(FilmeFiltroDTO filter)
        {
            try
            {
                var (filmes, totalCount) = await _filmeRepository.GetAllFilmes(filter).ConfigureAwait(false);
                if(filmes == null || filmes.Count == 0)
                {
                    return Result<ResultadoPaginaDTO<FilmeDTO>>.Ok(204, new ResultadoPaginaDTO<FilmeDTO>());
                }
                var filmeDTOs = filmes.Select(f => new FilmeDTO
                {
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Genero = f.Genero,
                    Ano = f.Ano,
                    Sinopse = f.Sinopse,
                    Duracao = f.Duracao,
                    DataAdicionado = f.DataCriacao

                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<FilmeDTO>
                {
                    Items = filmeDTOs,
                    TotalItems = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };

                return Result<ResultadoPaginaDTO<FilmeDTO>>.Ok(200, pagedResult);
            }
            catch (Exception ex)
            {
                return Result<ResultadoPaginaDTO<FilmeDTO>>.Fail(400, $"Erro ao buscar filmes: {ex.Message}");
            }
        }

        public async Task<Result<FilmeDTO>> GetFilmeById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return Result<FilmeDTO>.Fail(400, "ID inválido");
            }

            var filme = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filme == null)
            {
                return Result<FilmeDTO>.Fail(404,"Filme não encontrado");
            }
            var filmeDTO = new FilmeDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Genero = filme.Genero,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse,
                Duracao = filme.Duracao,
                DataAdicionado = filme.DataCriacao
            };
            return Result<FilmeDTO>.Ok(200,filmeDTO);

        }

        public async Task<Result<FilmeDTO>> UpdateFilme(FilmeUpdateDTO filme, Guid idFilme)
        {

            var filmeExistente = await _filmeRepository.GetFilmeById(idFilme).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return Result<FilmeDTO>.Fail(404, "Filme não encontrado");
            }
           
            filmeExistente.Titulo = filme.Titulo;
            filmeExistente.Genero = filme.Genero;
            filmeExistente.Ano = filme.Ano;
            filmeExistente.Sinopse = filme.Sinopse;
            filmeExistente.Duracao = filme.Duracao;
            await _filmeRepository.UpdateFilme(filmeExistente).ConfigureAwait(false);
            if(filmeExistente == null)
            {
                return Result<FilmeDTO>.Fail(400, "Já existe um filme com esse título");
            }
            return Result<FilmeDTO>.Ok(200, new FilmeDTO
            {
                Id = filmeExistente.Id,
                Titulo = filmeExistente.Titulo,
                Genero = filmeExistente.Genero,
                Ano = filmeExistente.Ano,
                Sinopse = filmeExistente.Sinopse,
                Duracao = filmeExistente.Duracao,
                DataAdicionado = filmeExistente.DataCriacao

            });
        }
    }
}
