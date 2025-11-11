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
        public string Generos { get; set; }
        [Range(1900, 2100, ErrorMessage = "Ano deve ser entre 1900 e 2100.")]
        [Required(ErrorMessage = "O ano é obrigatório.")]
        public string Ano { get; set; }
        [MaxLength(2000)]
        public string? Sinopse { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        public int Duracao { get; set; }
    }
}
