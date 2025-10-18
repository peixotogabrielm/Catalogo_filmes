using CatalogoFilmes.Models;

namespace CatalogoFilmes.DTOs
{
    public class ListaUsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime DataCriacao { get; set; }
        public int FilmesAdicionados { get; set; }
    }
}
