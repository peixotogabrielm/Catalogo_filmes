using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CatalogoFilmes.Models
{
    public class Usuario : IdentityUser
    {

      
        public string Nome { get; set; }
        
        public string Email { get; set; }

        public string Provider { get; set; }
    
        public string Celular { get; set; }
   
        public string CPF { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        public ICollection<Catalogo> Catalogos { get; set; }
    }
}
