using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IActionResult> GetAllFilmes([FromQuery] FilmeFiltroDTO filter)
        {

            var response = await _filmeService.GetAllFilmes(filter);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(new {response.Data});
        }
        [HttpGet("GetFilmeById/{id}")]
        public async Task<IActionResult> GetFilmeById(Guid id)
        {
            var response = await _filmeService.GetFilmeById(id);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(new { response.Data });
        }
        [HttpPost("AddFilme")]
        public async Task<IActionResult> AddFilme(CriarFilmeDTO filme)
        {
            var response = await _filmeService.AddFilme(filme);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Created("", new { response.Data });
        }
        [HttpPut("UpdateFilme")]
        public async Task<IActionResult> UpdateFilme(FilmeUpdateDTO filme)
        {
            var response = await _filmeService.UpdateFilme(filme);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(new { response.Data });
        }
        [HttpDelete("DeleteFilme/{id}")]
        public async Task<IActionResult> DeleteFilme(Guid id)
        {
            var response = await _filmeService.DeleteFilme(id);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response.Sucesso);
        }

    }
}
