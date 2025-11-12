using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static CatalogoFilmes.Helpers.Errors;

namespace CatalogoFilmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CatalogoController : Controller
    {
        private readonly ICatalogoService _catalogoService;
        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }


        [HttpGet]
        public async Task<Results<Ok<IEnumerable<CatalogoDTO>>, BadRequest<string>>> GetAllCatalogos([FromQuery] FilterCatalogoDTO filtroDto)
        {
            var catalogos = await _catalogoService.GetAllCatalogosAsync(filtroDto);
            if(catalogos.IsFailed)
            {
                return TypedResults.BadRequest(catalogos.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok(catalogos.Value);
        }

        [HttpGet("{id}")]
        public async Task<Results<Ok<IEnumerable<CatalogoDTO>>, NotFound<string>, BadRequest<string>>> GetCatalogosById(string id)
        {
            var catalogos = await _catalogoService.GetCatalogosByIdAsync(id);

            if (catalogos.HasError<NotFoundError>())
            {
                return TypedResults.NotFound(catalogos.Errors.FirstOrDefault()?.Message);
            }

            if (catalogos.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(catalogos.Errors.FirstOrDefault()?.Message);
            }
            
            return TypedResults.Ok(catalogos.Value);
        }

        [HttpPost]
        public async Task<Results<Created, BadRequest<string>>> AddUserCatalogo([FromBody] CriarCatalogoDTO catalogoDto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var addCatalogo = await _catalogoService.AddUserCatalogoAsync(catalogoDto, usuarioId);
            if(addCatalogo.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(addCatalogo.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Created();
        }

        [HttpPut]
        public async Task<Results<Ok, BadRequest<string>>> UpdateUserCatalogo([FromBody] CatalogoDTO catalogoDto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updateCatalogo = await _catalogoService.UpdateUserCatalogoAsync(catalogoDto, usuarioId);
            if(updateCatalogo.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(updateCatalogo.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<Results<Ok, BadRequest<string>>> DeleteUserCatalogo(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var deleteCatalogo = await _catalogoService.DeleteUserCatalogoAsync(id, usuarioId);
            if(deleteCatalogo.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(deleteCatalogo.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok();
        }

        [HttpPost("{catalogoId}/like")]
        public async Task<Results<Ok, BadRequest<string>>> LikeCatalogo(string catalogoId)
        {
            var result = await _catalogoService.LikeCatalogoAsync(catalogoId);
            if(result.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(result.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok();
        }

        [HttpPost("{catalogoId}/dislike")]
        public async Task<Results<Ok, BadRequest<string>>> DislikeCatalogo(string catalogoId)
        {
            var result = await _catalogoService.DislikeCatalogoAsync(catalogoId);
            if(result.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(result.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok();
        }


        [HttpGet("{catalogoId}/numero-favoritos")]
        public async Task<Results<Ok<int?>, BadRequest<string>>> GetNumeroFavoritos(string catalogoId)
        {
            var numeroFavoritos = await _catalogoService.GetNumeroFavoritosAsync(catalogoId);
            if(numeroFavoritos.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(numeroFavoritos.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok(numeroFavoritos?.Value);
        }
        
         // [HttpPost("{catalogoId}/favoritar")]
        // public async Task<IActionResult> FavoritarCatalogo(string catalogoId)
        // {
        //     await _catalogoService.FavoritarCatalogoAsync(catalogoId);
        //     return new OkResult();
        // }
        // [HttpPost("{catalogoId}/remover-favorito")]
        // public async Task<IActionResult> RemoverFavoritoCatalogo(string catalogoId)
        // {
        //     await _catalogoService.RemoverFavoritoCatalogoAsync(catalogoId);
        //     return new OkResult();
        // }

    }
}