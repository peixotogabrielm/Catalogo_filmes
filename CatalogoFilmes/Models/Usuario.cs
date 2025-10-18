using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string SenhaHash { get; set; }
        [Required]
        [MaxLength(25)]
        public string Role { get; set; } = "User";
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public List<Filme> FilmesCriados { get; set; } = new List<Filme>();
    }
}
