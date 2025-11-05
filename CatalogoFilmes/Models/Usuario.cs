using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CatalogoFilmes.Models
{
    public class Usuario : IdentityUser
    {

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string Provider { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Celular deve conter apenas números e pode incluir o código do país")]
        public string Celular { get; set; }
        [Required]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter exatamente 11 dígitos numéricos.")]
        public string CPF { get; set; }
        public List<Filme> FilmesCriados { get; set; } = new List<Filme>();
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
