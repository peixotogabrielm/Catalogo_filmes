using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class CriarFilmeDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "Ano deve ser entre 1900 e 2100.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        [StringLength(2000)]
        public string Sinopse { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        public TimeSpan Duracao { get; set; }
    }
}
