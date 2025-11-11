using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogoFilmes.DTOs
{
    public class UpdateRoleDTO
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        [DefaultValue("User")] 
        public string NovaRole { get; set; } = "User";
    }
}
