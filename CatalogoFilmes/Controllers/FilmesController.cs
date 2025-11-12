using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static CatalogoFilmes.Helpers.Errors;

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
        public async Task<Results<Ok<ResultadoPaginaDTO<FilmeDTO>>, BadRequest<string>>> GetAllFilmes([FromQuery] FilmeFiltroDTO filter)
        {

            var response = await _filmeService.GetAllFilmes(filter);

            if(response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }

            return TypedResults.Ok(response.Value);
        }

        [HttpGet("GetFilmeById/{id}")]
        [AllowAnonymous]
        public async Task<Results<Ok<FilmeDTO>, NotFound<string>, BadRequest<string>>> GetFilmeById(Guid id)
        {
            var response = await _filmeService.GetFilmeById(id);
            if (response.HasError<NotFoundError>())
            {
                return TypedResults.NotFound(response.Errors.FirstOrDefault()?.Message);

            }else if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok(response.Value);
        }

        [HttpPost("AddFilme")]
        public async Task<Results<Created<FilmeDTO>, BadRequest<string>>> AddFilme([FromBody] CriarFilmeDTO filme)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var usuarioId = Guid.Parse(userIdClaim.Value);
            var response = await _filmeService.AddFilme(filme, usuarioId);
            if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Created("", response.Value);
        }

        [HttpPut("UpdateFilme")]
        public async Task<Results<Ok<FilmeDTO>, NotFound<string>, BadRequest<string>>> UpdateFilme([FromBody]FilmeUpdateDTO filme, [FromQuery, Required] Guid idFilme)
        {
            var response = await _filmeService.UpdateFilme(filme, idFilme);
            if (response.HasError<NotFoundError>())
            {
                return TypedResults.NotFound(response.Errors.FirstOrDefault()?.Message);

            }else if(response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok(response.Value);
        }

        [HttpDelete("DeleteFilme/{id}")]
        public async Task<Results<Ok<bool>, NotFound<string>, BadRequest<string>>> DeleteFilme(Guid id)
        {
            var response = await _filmeService.DeleteFilme(id);
            
            if (response.HasError<NotFoundError>())
            {
                return TypedResults.NotFound(response.Errors.FirstOrDefault()?.Message);

            }else if(response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            return TypedResults.Ok(response.Value);
        }

    }
}
