using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

namespace CatalogoFilmes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;
        public AdminController(IAdminService adminService, IAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;

        }

        [HttpPost("RegistrarAdmin")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultCreatedStringExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestStringExample))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ResultUnauthorizedStringExample))]
        public async Task<IActionResult> RegistroAdmin([FromBody] RegistroDTO dto, [FromQuery] string chaveSecreta)
        {
           

            var response = await _authService.RegistroAdmin(dto, chaveSecreta);

            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }else if(response.StatusCode == 401)
            {
                return Unauthorized(response);
            }

            return Created("", response.Data);
        }

        [HttpGet("Dashboard")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<DashboardStatusDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessDashboardStatusDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<DashboardStatusDTO>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestDashboardStatusDTOExample))]
        public async Task<IActionResult> GetDashboardStats()
        {
            var response = await _adminService.GetDashboardStats();

            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }
      
        [HttpGet("Usuarios")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<ResultadoPaginaDTO<ListaUsuarioDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessResultadoPaginaUsuarioDTOExample))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(Result<ResultadoPaginaDTO<ListaUsuarioDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ResultNoContentPaginaUsuarioDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<ResultadoPaginaDTO<ListaUsuarioDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestPaginaListaUsuarioDTOExample))]
        public async Task<IActionResult> GetUsuarios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _adminService.GetUsuarios(pageNumber, pageSize);

            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }else if(response.StatusCode == 204)
            {
                return NoContent();
            }

            return Ok(response.Data);
        }

        [HttpPut("Usuarios/role")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessStringExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestStringExample))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ResultUnauthorizedStringExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ResultNotFoundStringExample))]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateRoleDTO dto)
        {
            var response = await _adminService.UpdateUserRole(dto);

            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }else if(response.StatusCode == 401)
            {
                return Unauthorized(response);
            }else if(response.StatusCode == 404)
            {
                return NotFound(response);
            }

            return Ok(response.Data);
        }
        [HttpDelete("Usuarios/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessStringExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestStringExample))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ResultUnauthorizedStringExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Result<string>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ResultNotFoundStringExample))]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _adminService.DeleteUsuario(id, currentUserId);

            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            else if (response.StatusCode == 401)
            {
                return Unauthorized(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }

            return Ok(response.Data);
        }
        [HttpGet("Filmes/stats")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Result<FilmesStatusDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultSuccessFilmesStatusDTOExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result<FilmesStatusDTO>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ResultBadRequestFilmeStatusDTOExample))]
        public async Task<IActionResult> GetFilmesStats()
        {
            var response = await _adminService.GetFilmesStats();

            if(response.StatusCode == 400)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }
    }
}
