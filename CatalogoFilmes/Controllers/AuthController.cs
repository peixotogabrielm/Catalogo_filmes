using Azure;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessStringExample))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ResultUnauthorizedStringExample))]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response.StatusCode == 401)
            {
                return Unauthorized(response.Mensagem);
            }
            return Ok(response.Data);
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultSuccessStringExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestStringExample))]
        public async Task<IActionResult> Registrar([FromBody] RegistroDTO dto)
        {
            var response = await _authService.RegistrarAsync(dto);
            if (response.StatusCode == 400)
            {
                return BadRequest(response.Mensagem);
            }
            return Created("", response.Data);
        }
    }
}
