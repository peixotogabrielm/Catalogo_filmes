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
    [ApiConventionType(typeof(ApiConvention))]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet("Dashboard")]
        public async Task<IResult> GetDashboardStats()
        {
            var response = await _adminService.GetDashboardStats();

            return response.ToApiResult();
        }
      
        [HttpGet("Usuarios")]
        public async Task<IResult> GetUsuarios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _adminService.GetUsuarios(pageNumber, pageSize);

            return response.ToApiResult();
        }

        [HttpPut("Usuarios/role")]
        public async Task<IResult> UpdateUserRole([FromBody] UpdateRoleDTO dto)
        {
            var response = await _adminService.UpdateUserRole(dto);

            return response.ToApiResult();
        }

        [HttpDelete("Usuarios/{id}")]
        public async Task<IResult> DeleteUsuario(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claimsIdentity = JwtHelper.getUserIdToken(identity);
            if (claimsIdentity == null)
            {
                return TypedResults.Unauthorized();
            }

            var usuarioId = Guid.Parse(claimsIdentity);

            var response = await _adminService.DeleteUsuario(id, usuarioId);
            return response.ToApiResult();
            
        }
        
        [HttpGet("Filmes/stats")]
        public async Task<IResult> GetFilmesStats()
        {
            var response = await _adminService.GetFilmesStats();

            return response.ToApiResult();
        }
    }
}
