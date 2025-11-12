using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.DTOs
{
    public class UpdateCatalogoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Tags Tags { get; set; }
    }
}