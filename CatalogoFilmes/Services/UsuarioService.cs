using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Data;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using CatalogoFilmes.Models;
using CatalogoFilmes.Repositories.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CatalogoFilmes.Helpers.Errors;

namespace CatalogoFilmes.Services.Interfaces
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
            private readonly UserManager<Usuario> _userManager;
        public UsuarioService(IUsuarioRepository usuarioRepository, UserManager<Usuario> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            
        }
        public async Task<Result<string>> RegistrarAsync(RegistroDTO request)
        {
            if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return Result.Fail<string>(new BadRequestError("Nome, email e senha são obrigatórios"));
            }

            if (string.IsNullOrEmpty(request.CPF) || string.IsNullOrEmpty(request.Celular))
            {
                return Result.Fail<string>(new BadRequestError("CPF e celular são obrigatórios"));
            }

            if (request.Senha != request.ConfirmarSenha)
            {
                return Result.Fail<string>(new BadRequestError("As senhas não coincidem"));
            }

            var isEmailExist = await _usuarioRepository.IsEmailExistAsync(request.Email);

            if (isEmailExist)
            {
                return Result.Fail<string>(new BadRequestError("Email já está em uso"));
            }

            var user = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                CPF = request.CPF,
                Celular = request.Celular,
            };

            var result = await _userManager.CreateAsync(user, request.Senha);

            if (!result.Succeeded)
            {
                return Result.Fail<string>(new BadRequestError("Falha ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description))));
            }
            
            return Result.Ok<string>("Conta criada com sucesso!");
        }
    }
}