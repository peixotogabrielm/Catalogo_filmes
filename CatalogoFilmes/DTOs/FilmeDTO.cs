using CatalogoFilmes.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class FilmeDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Genero { get; set; }
        [Range(1900, 2100, ErrorMessage = "Ano deve ser entre 1900 e 2100.")]
        public int Ano { get; set; }
        public string? Sinopse { get; set; }
        public string? Nome { get; set; }
        public DateTime DataCriado { get; set; }
    }
}
