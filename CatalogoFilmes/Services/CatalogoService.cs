using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _catalogoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CatalogoService(ICatalogoRepository catalogoRepository, IUsuarioRepository usuarioRepository)
        {
            _catalogoRepository = catalogoRepository;
            _usuarioRepository = usuarioRepository;
        }
        
        public async Task<Result> AddUserCatalogoAsync(CriarCatalogoDTO criarCatalogoDto, string? usuarioId)
        {
             if (string.IsNullOrEmpty(usuarioId))
            {
                return Result.Fail(new UnauthorizedError("Usuário não autenticado."));
            }
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
            if (usuario == null)
            {
                return Result.Fail(new BadRequestError("Usuário não encontrado."));
            }
            var catalogo = new Catalogo
            {
                Nome = criarCatalogoDto.nomeCatalogo,
                Descricao = criarCatalogoDto.Descricao,
                UsuarioId = usuarioId,
            };
            await _catalogoRepository.AddCatalogoAsync(catalogo);
            return Result.Ok().WithSuccess(new NoContentSuccess());

        }

        public async Task <Result>DeleteUserCatalogoAsync(string id, string usuarioId)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
            if (usuario == null)
            {
                return Result.Fail(new BadRequestError("Usuário não encontrado."));
            }
            await _catalogoRepository.DeleteCatalogoAsync(id);
            return Result.Ok().WithSuccess(new OkSuccess("Catálogo deletado com sucesso."));
        }

        public async Task<Result> DislikeCatalogoAsync(string catalogoId)
        {
            await _catalogoRepository.DislikeCatalogoAsync(catalogoId);
            return Result.Ok().WithSuccess(new OkSuccess());
        }

        public async Task<Result<IEnumerable<CatalogoDTO>>> GetAllCatalogosAsync(FilterCatalogoDTO filtroDto)
        {
            try
            {
                var catalogos = await _catalogoRepository.GetAllCatalogos(filtroDto);
                if( catalogos == null || !catalogos.Any())
                {
                    return Result.Ok().WithSuccess(new OkSuccess(new List<CatalogoDTO>()));
                }
                var catalogoDtos = catalogos.Select(c => new CatalogoDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    Visualizacoes = c.Visualizacoes,
                    Likes = c.Likes,
                    Dislikes = c.Dislikes,
                    NumeroFavoritos = c.NumeroFavoritos,
                    Tags = c.Tags,
                    Filmes = c.Filmes,
                    NomeUsuario = c.Usuario.Nome
                });

                return Result.Ok().WithSuccess(new OkSuccess(catalogoDtos));
            }catch(Exception ex)
            {
                return Result.Fail(new BadRequestError("Erro ao carregar catálogos."));
            }
            
        }

        public async Task<Result<IEnumerable<CatalogoDTO>>> GetCatalogosByIdAsync(string id)
        {
            var catalogos = await _catalogoRepository.GetAllCatalogosByUserIdAsync(id);
            if( catalogos == null || !catalogos.Any())
            {
                return Result.Fail(new NotFoundError("Nenhum catálogo encontrado para o usuário especificado."));
            }
            var catalogoDtos = catalogos.Select(c => new CatalogoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Visualizacoes = c.Visualizacoes,
                Likes = c.Likes,
                Dislikes = c.Dislikes,
                NumeroFavoritos = c.NumeroFavoritos,
                Tags = c.Tags,
                Filmes = c.Filmes,
                NomeUsuario = c.Usuario.Nome
            });

            return Result.Ok().WithSuccess(new OkSuccess(catalogoDtos));
        }

        public async Task<Result<int?>> GetNumeroFavoritosAsync(string catalogoId)
        {
            var numeroFavoritos = await _catalogoRepository.GetNumeroFavoritosAsync(catalogoId);
            if(numeroFavoritos == null)
            {
                return Result.Fail(new BadRequestError("Catálogo não encontrado."));
            }
            return Result.Ok().WithSuccess(new OkSuccess(numeroFavoritos));
        }

        public async Task<Result> LikeCatalogoAsync(string catalogoId)
        {
            await _catalogoRepository.LikeCatalogoAsync(catalogoId).ConfigureAwait(false);
            return Result.Ok().WithSuccess(new OkSuccess());
        }

        public async Task<Result> UpdateUserCatalogoAsync(CatalogoDTO catalogoDto, string usuarioId)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
            if (usuario == null)
            {
                return Result.Fail(new BadRequestError("Usuário não encontrado."));
            }
            var catalogo = new Catalogo
            {
                Id = catalogoDto.Id,
                Nome = catalogoDto.Nome,
                Descricao = catalogoDto.Descricao,
                Tags = catalogoDto.Tags,
            };
            await _catalogoRepository.UpdateCatalogoAsync(catalogo);
            return Result.Ok().WithSuccess(new OkSuccess("Catálogo atualizado com sucesso."));
        }

         public async Task<Result<IEnumerable<Tags>>> GetAllTagsAsync()
        {
            var tags =  await EnumHelper.GetEnumValues<Tags>();
            return Result.Ok().WithSuccess(new OkSuccess(tags.Cast<Tags>()));
        }

        public async Task<Result> FavoritarCatalogoAsync(string catalogoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoverFavoritoCatalogoAsync(string catalogoId)
        {
            throw new NotImplementedException();
        }

       
    }
}