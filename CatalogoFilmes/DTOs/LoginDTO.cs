using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

      
    }
}
