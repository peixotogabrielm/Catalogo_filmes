using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(AppDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDTO request)
        {
            
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            {
                return ApiResponse<string>.Fail("Email ou senha inválidos");
            }

            return ApiResponse<string>.Ok(_jwtHelper.GenerateToken(usuario));

        }

        public async Task<ApiResponse<string>> RegistrarAsync(RegistroDTO request)
        {
            if(string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return ApiResponse<string>.Fail("Nome, email e senha são obrigatórios");
            }
            var isEmailExist = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);
            if (isEmailExist)
            {
                return ApiResponse<string>.Fail("Email já está em uso");
            }
            var user = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                Role = "User",
                DataCriacao = DateTime.UtcNow
            };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return ApiResponse<string>.Ok("Conta criada com sucesso!");
        }

        public async Task<ApiResponse<string>> RegistroAdmin(RegistroDTO request)
        {
            if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return ApiResponse<string>.Fail("Nome, email e senha são obrigatórios");
            }

            var isEmailExist = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);
            if (isEmailExist)
            {
                return ApiResponse<string>.Fail("Email já está em uso");
            }
            var user = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                Role = "Admin",
                DataCriacao = DateTime.UtcNow
            };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return ApiResponse<string>.Ok("Conta criada com sucesso!");
        }
    }
}
