using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.Models
{
    public class Filme
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public int Ano { get; set; }
        public string Sinopse { get; set; }

    }
}
