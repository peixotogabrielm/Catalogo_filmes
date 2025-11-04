using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CatalogoFilmes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

      [HttpPost("Registrar")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultSuccessStringExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestStringExample))]
        public async Task<IActionResult> Registrar([FromBody] RegistroDTO dto)
        {
            var response = await _usuarioService.RegistrarAsync(dto);
            if (response.StatusCode == 400)
            {
                return BadRequest(response.Mensagem);
            }
            return Created("", response.Data);
        }
    }
}