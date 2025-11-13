using CatalogoFilmes.Documentacao;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

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
        public async Task<IResult> GetAllCatalogos([FromQuery] FilterCatalogoDTO filtroDto)
        {
            var catalogos = await _catalogoService.GetAllCatalogosAsync(filtroDto);
            return catalogos.ToApiResult();
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetCatalogosById(string id)
        {
            var catalogos = await _catalogoService.GetCatalogosByIdAsync(id);
            return catalogos.ToApiResult();
        }

        [HttpPost]
        public async Task<IResult> AddUserCatalogo([FromBody] CriarCatalogoDTO catalogoDto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var addCatalogo = await _catalogoService.AddUserCatalogoAsync(catalogoDto, usuarioId!);
            return addCatalogo.ToApiResult();
        }

        [HttpPut]
        public async Task<IResult> UpdateUserCatalogo([FromBody] CatalogoDTO catalogoDto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updateCatalogo = await _catalogoService.UpdateUserCatalogoAsync(catalogoDto, usuarioId!);
            return updateCatalogo.ToApiResult();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUserCatalogo(string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var deleteCatalogo = await _catalogoService.DeleteUserCatalogoAsync(id, usuarioId!);
            return deleteCatalogo.ToApiResult();
        }

        [HttpPost("{catalogoId}/like")]
        public async Task<IResult> LikeCatalogo(string catalogoId)
        {
            var result = await _catalogoService.LikeCatalogoAsync(catalogoId);
            return result.ToApiResult();
        }

        [HttpPost("{catalogoId}/dislike")]
        public async Task<IResult> DislikeCatalogo(string catalogoId)
        {
            var result = await _catalogoService.DislikeCatalogoAsync(catalogoId);
            return result.ToApiResult();
        }

        [HttpGet("{catalogoId}/numero-favoritos")]
        public async Task<IResult> GetNumeroFavoritos(string catalogoId)
        {
            var numeroFavoritos = await _catalogoService.GetNumeroFavoritosAsync(catalogoId);
            return numeroFavoritos.ToApiResult();
        }

        [HttpGet("Tags")]
        public async Task<IResult> GetAllTags()
        {
            var tags = await _catalogoService.GetAllTagsAsync();
            return tags.ToApiResult();
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