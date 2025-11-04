using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.DTOs;
using CatalogoFilmes.Helpers;

namespace CatalogoFilmes.Services.Interfaces
{
    public interface IUsuarioService 
    {
        Task<Result<string>> RegistrarAsync(RegistroDTO request);
        
    }
}