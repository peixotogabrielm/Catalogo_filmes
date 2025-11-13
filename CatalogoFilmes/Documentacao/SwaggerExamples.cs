using System;
using System.Collections.Generic;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CatalogoFilmes.Documentacao
{
    // ========== AUTENTICAÇÃO ==========

    public class LoginExample : IExamplesProvider<LoginDTO>
    {
        public LoginDTO GetExamples() => new()
        {
            Email = "usuario@email.com",
            Senha = "Senha123"
        };
    }

    public class RegistroExample : IExamplesProvider<RegistroDTO>
    {
        public RegistroDTO GetExamples() => new()
        {
            Nome = "João Silva",
            Email = "joao.silva@email.com",
            CPF = "12345678901",
            Celular = "+5511987654321",
            Senha = "Senha123",
            ConfirmarSenha = "Senha123"
        };
    }

    // ========== FILMES ==========

    public class CriarFilmeExample : IExamplesProvider<CriarFilmeDTO>
    {
        public CriarFilmeDTO GetExamples() => new()
        {
            Titulo = "A Origem",
            Generos = "Ficção Científica, Ação",
            Ano = "2010",
            Sinopse = "Um ladrão que rouba segredos corporativos através do uso da tecnologia de compartilhamento de sonhos.",
            Duracao = 148
        };
    }

    public class FilmeUpdateExample : IExamplesProvider<FilmeUpdateDTO>
    {
        public FilmeUpdateDTO GetExamples() => new()
        {
            Titulo = "A Origem",
            Generos = "Ficção Científica, Ação, Suspense",
            Ano = "2010",
            Sinopse = "Um ladrão que rouba segredos corporativos através do uso da tecnologia de compartilhamento de sonhos.",
            Duracao = 148
        };
    }

    public class FilmeFiltroExample : IExamplesProvider<FilmeFiltroDTO>
    {
        public FilmeFiltroDTO GetExamples() => new()
        {
            Titulo = "Origem",
            Genero = new List<string> { "Ficção Científica", "Ação" },
            Ano = "2010",
            PageNumber = 1,
            PageSize = 10
        };
    }

    // ========== CATÁLOGO ==========

    public class CriarCatalogoExample : IExamplesProvider<CriarCatalogoDTO>
    {
        public CriarCatalogoDTO GetExamples() => new()
        {
            nomeCatalogo = "Melhores Filmes de Ficção Científica",
            Descricao = "Uma coleção dos melhores filmes do gênero ficção científica"
        };
    }

    public class UpdateCatalogoExample : IExamplesProvider<UpdateCatalogoDTO>
    {
        public UpdateCatalogoDTO GetExamples() => new()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Nome = "Clássicos da Ficção Científica",
            Descricao = "Filmes clássicos que definiram o gênero",
            Tags = Tags.FiccaoCientifica
        };
    }

    public class FilterCatalogoExample : IExamplesProvider<FilterCatalogoDTO>
    {
        public FilterCatalogoDTO GetExamples() => new()
        {
            nomeCatalogo = "Ficção",
            tagCatalogo = Tags.FiccaoCientifica,
            minLikes = 10,
            minFavoritos = 5
        };
    }

    // ========== ADMIN ==========

    public class UpdateRoleExample : IExamplesProvider<UpdateRoleDTO>
    {
        public UpdateRoleDTO GetExamples() => new()
        {
            UsuarioId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            NovaRole = "Admin"
        };
    }
}
