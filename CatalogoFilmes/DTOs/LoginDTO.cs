using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo senha é obrigatório")]
        public string Senha { get; set; }
    }
}
