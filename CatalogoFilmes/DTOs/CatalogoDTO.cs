using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;

namespace CatalogoFilmes.DTOs
{
    public class CatalogoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Visualizacoes { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int NumeroFavoritos { get; set; }
        public Tags Tags { get; set; }
        public ICollection<Filme> Filmes { get; set; }
        public string NomeUsuario { get; set; }

    }

}