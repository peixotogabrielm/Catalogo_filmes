using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MinLength(2, ErrorMessage = "Nome deve ter no mínimo 2 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MinLength(5, ErrorMessage = "Email deve ter no mínimo 5 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])[A-Za-z0-9]{6,}$",
            ErrorMessage = "A senha deve ter ao menos 6 caracteres alfanuméricos e conter pelo menos uma letra maiúscula.")]
        public string Senha { get; set; }

    }
}
