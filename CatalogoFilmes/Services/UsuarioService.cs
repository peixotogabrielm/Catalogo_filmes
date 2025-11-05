using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Services.Interfaces
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result<string>> RegistrarAsync(RegistroDTO request)
        {
            if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return Result<string>.Fail(400, "Nome, email e senha são obrigatórios");
            }
            if(string.IsNullOrEmpty(request.CPF) || string.IsNullOrEmpty(request.Celular))
            {
                return Result<string>.Fail(400, "CPF e celular são obrigatórios");
            }
            if(request.Senha != request.ConfirmarSenha)
            {
                return Result<string>.Fail(400, "As senhas não coincidem");
            }
            var isEmailExist = await _usuarioRepository.IsEmailExistAsync(request.Email);
            if (isEmailExist)
            {
                return Result<string>.Fail(400, "Email já está em uso");
            }
            var user = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                CPF = request.CPF,
                Celular = request.Celular,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                RoleId = new Roles { Role = "User" }.Id
            };
            await _usuarioRepository.AddUsuarioAsync(user);
            await _usuarioRepository.SalvarUsuarioAsync();
            return Result<string>.Ok(200, "Conta criada com sucesso!");
        }
    }
}