using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.DTOs
{
    public class FilterCatalogoDTO
    {
        public string? nomeUsuario { get; set; }
        public string? nomeCatalogo { get; set; }
        public Tags? tagCatalogo { get; set; }
        public int? minLikes { get; set; }
        public int? minFavoritos { get; set; }
    }
}