using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using static CatalogoFilmes.Helpers.Errors;

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
        public async Task<Results<Ok<string>, BadRequest<string>>> Registrar([FromBody] RegistroDTO dto)
        {
            var response = await _usuarioService.RegistrarAsync(dto);
            if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            
            return TypedResults.Ok(response.Value);
        }
    }
}