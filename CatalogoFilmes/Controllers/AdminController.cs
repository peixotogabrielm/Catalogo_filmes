using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Services;
using CatalogoFilmes.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using static CatalogoFilmes.Helpers.Errors;

namespace CatalogoFilmes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet("Dashboard")]
        public async Task<Results<Ok<DashboardStatusDTO>, BadRequest<string>>> GetDashboardStats()
        {
            var response = await _adminService.GetDashboardStats();

            if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }

            return TypedResults.Ok(response.Value);
        }
      
        [HttpGet("Usuarios")]
        public async Task<Results<Ok<ResultadoPaginaDTO<ListaUsuarioDTO>>, BadRequest<string>>> GetUsuarios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _adminService.GetUsuarios(pageNumber, pageSize);

            if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }

            return TypedResults.Ok(response.Value);
        }

        [HttpPut("Usuarios/role")]
        public async Task<Results<Ok<string>, BadRequest<string>, UnauthorizedHttpResult, NotFound<string>>> UpdateUserRole([FromBody] UpdateRoleDTO dto)
        {
            var response = await _adminService.UpdateUserRole(dto);

            if (response.IsFailed)
            {
                if(response.HasError<UnauthorizedError>())
                {
                    return TypedResults.Unauthorized();
                }
                else if (response.HasError<NotFoundError>())
                {
                    return TypedResults.NotFound(response.Errors.FirstOrDefault()?.Message);
                }else
                {
                    return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
                }
            }

            return TypedResults.Ok(response.Value);
        }

        [HttpDelete("Usuarios/{id}")]
        public async Task<Results<Ok<string>, BadRequest<string>, UnauthorizedHttpResult, NotFound<string>>> DeleteUsuario(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claimsIdentity = JwtHelper.getUserIdToken(identity);
            if (claimsIdentity == null)
            {
                return TypedResults.Unauthorized();
            }

            var usuarioId = Guid.Parse(claimsIdentity);

            var response = await _adminService.DeleteUsuario(id, usuarioId);


            if (response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }
            else if (response.HasError<UnauthorizedError>())
            {
                return TypedResults.Unauthorized();
            }
            else if (response.HasError<NotFoundError>())
            {
                return TypedResults.NotFound(response.Errors.FirstOrDefault()?.Message);
            }

            return TypedResults.Ok(response.Value);
        }
        
        [HttpGet("Filmes/stats")]
        public async Task<Results<Ok<FilmesStatusDTO>, BadRequest<string>>> GetFilmesStats()
        {
            var response = await _adminService.GetFilmesStats();

            if(response.HasError<BadRequestError>())
            {
                return TypedResults.BadRequest(response.Errors.FirstOrDefault()?.Message);
            }

            return TypedResults.Ok(response.Value);
        }
    }
}
