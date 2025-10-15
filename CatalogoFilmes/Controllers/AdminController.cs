using CatalogoFilmes.DTOs;
using CatalogoFilmes.Services;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> RegistroAdmin([FromBody] RegistroDTO dto, [FromQuery] string chaveSecreta)
        {
            if (chaveSecreta != "lM3ULXRHup")
            {
                return Unauthorized("Chave secreta inválida");
            }

            var response = await _authService.RegistroAdmin(dto);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Created("", response.Data);
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var response = await _adminService.GetDashboardStats();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }
      
        [HttpGet("Usuarios")]
        public async Task<IActionResult> GetUsuarios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var response = await _adminService.GetUsuarios(pageNumber, pageSize);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }
        [HttpPut("Usuarios/role")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateRoleDTO dto)
        {
            var response = await _adminService.UpdateUserRole(dto);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(new { message = response.Data });
        }
        [HttpDelete("Usuarios/{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _adminService.DeleteUsuario(id, currentUserId);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(new { message = response.Data });
        }
        [HttpGet("Filmes/stats")]
        public async Task<IActionResult> GetFilmesStats()
        {
            var response = await _adminService.GetFilmesStats();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response.Data);
        }
    }
}
