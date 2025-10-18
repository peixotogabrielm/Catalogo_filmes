using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmes.Models
{
    public class Filme
    {
        [Key]
        public Guid Id { get; set; }
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
        public string? Sinopse { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public Guid? CriadoPorId { get; set; }
        [ForeignKey("CriadoPorId")]
        public Usuario CriadoPor { get; set; }

    }
}
