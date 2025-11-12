using Azure;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<Results<Ok<string>, UnauthorizedHttpResult>> Login([FromBody] LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response.IsFailed)
            {
                return TypedResults.Unauthorized();
            }
            return TypedResults.Ok(response.Value);
        }

        
    }
}
