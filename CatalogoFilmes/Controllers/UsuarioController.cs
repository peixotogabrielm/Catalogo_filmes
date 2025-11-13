using CatalogoFilmes.Documentacao;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CatalogoFilmes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<IResult> Registrar([FromBody] RegistroDTO dto)
        {
            var response = await _usuarioService.RegistrarAsync(dto);
            return response.ToApiResult();
        }
    }
}