using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string SenhaHash { get; set; }
        [Required]
        public string Role { get; set; } = "User"; 

        public DateTime DataCriacao { get; set; }
    }
}
