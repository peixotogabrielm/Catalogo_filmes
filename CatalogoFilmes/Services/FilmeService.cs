using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using CatalogoFilmes.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using static CatalogoFilmes.Helpers.Errors;
using static CatalogoFilmes.Helpers.Successes;

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
                Genero = filme.Generos,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse,
                Duracao = filme.Duracao
            };
            var retornoFilme = await _filmeRepository.AddFilme(novoFilme).ConfigureAwait(false);
            if (retornoFilme == null)
            {
                return Result.Fail<FilmeDTO>(new BadRequestError("Filme já existe"));
            }
            var filmeAdicionado = new FilmeDTO
            {
                Id = retornoFilme.Id,
                Titulo = retornoFilme.Titulo,
                Generos = retornoFilme.Genero,
                Ano = retornoFilme.Ano,
                Sinopse = retornoFilme.Sinopse,
                Duracao = retornoFilme.Duracao,
            };

            return Result.Ok().WithSuccess(new CreatedSuccess(filmeAdicionado));
        }


        public async Task<Result> DeleteFilme(Guid id)
        {
            var filmeExistente = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return Result.Fail(new NotFoundError("Filme não encontrado"));
            }
            var retornoDelete = await _filmeRepository.DeleteFilme(id).ConfigureAwait(false);
            if(retornoDelete == false)
            {
                return Result.Fail(new BadRequestError("Erro ao deletar filme"));
            }
            return Result.Ok().WithSuccess(new OkSuccess());

        }

        public async Task<Result<ResultadoPaginaDTO<FilmeDTO>>> GetAllFilmes(FilmeFiltroDTO filter)
        {
            try
            {
                var (filmes, totalCount) = await _filmeRepository.GetAllFilmes(filter).ConfigureAwait(false);
                if(filmes == null || filmes.Count == 0)
                {
                    return Result.Ok().WithSuccess(new OkSuccess(new ResultadoPaginaDTO<FilmeDTO>()));
                }
                var filmeDTOs = filmes.Select(f => new FilmeDTO
                {
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Generos = f.Genero,
                    Ano = f.Ano,
                    Sinopse = f.Sinopse,
                    Duracao = f.Duracao,
                    Idioma = f.Idioma,
                    PosterUrl = f.Poster
                }).ToList();

                var pagedResult = new ResultadoPaginaDTO<FilmeDTO>
                {
                    Data = filmeDTOs,
                    TotalItems = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };

                return Result.Ok().WithSuccess(new OkSuccess(pagedResult));
            }
            catch (Exception ex)
            {
                return Result.Fail(new BadRequestError($"Erro ao buscar filmes: {ex.Message}"));
            }
        }

        public async Task<Result<FilmeDTO>> GetFilmeById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return Result.Fail<FilmeDTO>(new BadRequestError("ID inválido"));
            }

            var filme = await _filmeRepository.GetFilmeById(id).ConfigureAwait(false);
            if (filme == null)
            {
                return Result.Fail<FilmeDTO>(new NotFoundError("Filme não encontrado"));
            }
            var filmeDTO = new FilmeDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Generos = filme.Genero,
                Ano = filme.Ano,
                Sinopse = filme.Sinopse,
                Duracao = filme.Duracao,
            };
            return Result.Ok().WithSuccess(new OkSuccess(filmeDTO));
        }

        public async Task<Result<FilmeDTO>> UpdateFilme(FilmeUpdateDTO filme, Guid idFilme)
        {

            var filmeExistente = await _filmeRepository.GetFilmeById(idFilme).ConfigureAwait(false);
            if (filmeExistente == null)
            {
                return Result.Fail<FilmeDTO>(new NotFoundError("Filme não encontrado"));
            }
           
            filmeExistente.Titulo = filme.Titulo;
            filmeExistente.Genero = filme.Generos;
            filmeExistente.Ano = filme.Ano;
            filmeExistente.Sinopse = filme.Sinopse;
            filmeExistente.Duracao = filme.Duracao;

            await _filmeRepository.UpdateFilme(filmeExistente).ConfigureAwait(false);

            if (filmeExistente == null)
            {
                return Result.Fail<FilmeDTO>(new BadRequestError("Já existe um filme com esse título"));
            }
            var filmeAtualizado = new FilmeDTO
            {
                Id = filmeExistente.Id,
                Titulo = filmeExistente.Titulo,
                Generos = filmeExistente.Genero,
                Ano = filmeExistente.Ano,
                Sinopse = filmeExistente.Sinopse,
                Duracao = filmeExistente.Duracao,

            };

            return Result.Ok().WithSuccess(new OkSuccess(filmeAtualizado));
        }
    }
}
