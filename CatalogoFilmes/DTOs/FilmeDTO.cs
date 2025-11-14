using CatalogoFilmes.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class FilmeDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Generos { get; set; }
        public string Ano { get; set; }
        public string Sinopse { get; set; }
        public int Duracao { get; set; }
        public string Idioma { get; set; }
        public string PosterUrl { get; set; }
        public string Trailer { get; set; }
        public List<EquipeDTO> Equipe { get; set; }
        public List<NotasFilmeExternoDTO> NotasExternas { get; set; }
    }

    public class EquipeDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }

    public class NotasFilmeExternoDTO
    {
        public Guid Id { get; set; }
        public string Fonte { get; set; }
        public string Nota { get; set; }
    }
}
