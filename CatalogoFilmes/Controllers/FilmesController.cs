using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoFilmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FilmesController : Controller
    {
        private readonly IFilmeService _filmeService;
        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet("GetAllFilmes")]
        [Produces("application/json")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<ResultadoPaginaDTO<FilmeDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessPaginaFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(Result<ResultadoPaginaDTO<FilmeDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ResultNoContentPaginaFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<ResultadoPaginaDTO<FilmeDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestPaginaFilmeDTOExample))]
        public async Task<IActionResult> GetAllFilmes([FromQuery] FilmeFiltroDTO filter)
        {

            var response = await _filmeService.GetAllFilmes(filter);
            if (response.StatusCode == 204)
            {
                return NoContent();
            }
            else if(response.StatusCode == 400)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }

        [HttpGet("GetFilmeById/{id}")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ResultNotFoundFilmeDTOExample))]
        public async Task<IActionResult> GetFilmeById(Guid id)
        {
            var response = await _filmeService.GetFilmeById(id);
            if (response.StatusCode == 404)
            {
                return NotFound(response);

            }else if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }

        [HttpPost("AddFilme")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultCreatedFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestFilmeDTOExample))]
        public async Task<IActionResult> AddFilme([FromBody] CriarFilmeDTO filme)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var usuarioId = Guid.Parse(userIdClaim.Value);
            var response = await _filmeService.AddFilme(filme, usuarioId);
            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            return Created("", response.Data);
        }

        [HttpPut("UpdateFilme")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestFilmeDTOExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Result<FilmeDTO>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ResultNotFoundFilmeDTOExample))]
        public async Task<IActionResult> UpdateFilme([FromBody]FilmeUpdateDTO filme, [FromQuery, Required] Guid idFilme)
        {
            var response = await _filmeService.UpdateFilme(filme, idFilme);
            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }else if(response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            return Ok(response.Data );
        }

        [HttpDelete("DeleteFilme/{id}")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessBoolExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<bool>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestBoolExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Result<bool>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ResultNotFoundBoolExample))]
        public async Task<IActionResult> DeleteFilme(Guid id)
        {
            var response = await _filmeService.DeleteFilme(id);
            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }else if(response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            return Ok(response.Sucesso);
        }

    }
}
