using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class CriarFilmeDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Generos { get; set; }
        [Required(ErrorMessage = "O ano é obrigatório.")]
        public string Ano { get; set; }

        [StringLength(2000)]
        public string Sinopse { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        public int Duracao { get; set; }
    }
}
