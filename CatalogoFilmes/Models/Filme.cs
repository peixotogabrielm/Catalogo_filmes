using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmes.Models
{
    public class Filme : Base
    {
        
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Genero { get; set; }
        [Required]
        [Range(1800, 2100)]
        public int Ano { get; set; }
        [MaxLength(2000)]
        [Required]
        public string? Sinopse { get; set; }
        [Required]
        public TimeSpan Duracao { get; set; }


    }
}
