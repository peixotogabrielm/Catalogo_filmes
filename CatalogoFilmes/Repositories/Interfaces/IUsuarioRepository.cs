using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> IsEmailExistAsync(string email);
        Task AddUsuarioAsync(Usuario usuario);
        Task SalvarUsuarioAsync();
    }
}