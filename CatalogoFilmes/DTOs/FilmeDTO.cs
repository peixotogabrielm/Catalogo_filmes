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
        [Required(ErrorMessage = "O ano é obrigatório.")]
        public int Ano { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        [MaxLength(2000)]
        public string? Sinopse { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        public TimeSpan Duracao { get; set; }
        public DateTime DataAdicionado { get; set; }
    }
}
