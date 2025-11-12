using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;
using FluentResults;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IUsuarioService 
    {
        Task<Result> RegistrarAsync(RegistroDTO request);
        
    }
}