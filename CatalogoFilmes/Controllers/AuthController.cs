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
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
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

        
    }
}
