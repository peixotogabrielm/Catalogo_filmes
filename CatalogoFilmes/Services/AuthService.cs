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

        public async Task<Result<string>> LoginAsync(LoginDTO request)
        {
            
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            {
                return Result<string>.Fail(401, "Email ou senha inválidos");
            }

            return Result<string>.Ok(200, _jwtHelper.GenerateToken(usuario));

        }

       

        
    }
}
