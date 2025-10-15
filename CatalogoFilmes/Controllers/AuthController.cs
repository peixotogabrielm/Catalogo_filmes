using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (!response.Sucesso)
            {
                return Unauthorized(new {error = response.Mensagem});
            }
            return Ok(new {token = response.Data});
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(RegistroDTO dto)
        {
            var response = await _authService.RegistrarAsync(dto);
            if (!response.Sucesso)
            {
                return BadRequest(new { error = response.Mensagem });
            }
            return Created("", new {mensagem = response.Data});
        }
    }
}
