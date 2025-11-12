using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CatalogoFilmes.Helpers.Errors;

namespace CatalogoFilmes.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly JwtHelper _jwtHelper;

        public AuthService(AppDbContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<string>> LoginAsync(LoginDTO request)
        {
            
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, request.Senha))
            {
                return Result.Fail<string>(new UnauthorizedError("Email ou senha inválidos"));
            }

            return Result.Ok<string>( _jwtHelper.GenerateToken(usuario));

        }

       

        
    }
}
