using CatalogoFilmes.Documentacao;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CatalogoFilmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FilmesController : Controller
    {
        private readonly IFilmeService _filmeService;
        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet("GetAllFilmes")]
        [AllowAnonymous]
        public async Task<IResult> GetAllFilmes([FromQuery] FilmeFiltroDTO filter)
        {
            var response = await _filmeService.GetAllFilmes(filter);
            return response.ToApiResult();
        }

        [HttpGet("GetFilmeById/{id}")]
        [AllowAnonymous]
        public async Task<IResult> GetFilmeById(Guid id)
        {
            var response = await _filmeService.GetFilmeById(id);
            return response.ToApiResult();
        }

        [HttpPost("AddFilme")]
        public async Task<IResult> AddFilme([FromBody] CriarFilmeDTO filme)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var usuarioId = Guid.Parse(userIdClaim.Value);
            var response = await _filmeService.AddFilme(filme, usuarioId);
            return response.ToApiResult();
        }

        [HttpPut("UpdateFilme")]
        public async Task<IResult> UpdateFilme([FromBody]FilmeUpdateDTO filme, [FromQuery, Required] Guid idFilme)
        {
            var response = await _filmeService.UpdateFilme(filme, idFilme);
            return response.ToApiResult();
        }

        [HttpDelete("DeleteFilme/{id}")]
        public async Task<IResult> DeleteFilme(Guid id)
        {
            var response = await _filmeService.DeleteFilme(id);
            return response.ToApiResult();

        }

    }
}
