using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmes.DTOs
{
    public class FilmeUpdateDTO
    {

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Generos { get; set; }
        [Required(ErrorMessage = "O ano é obrigatório.")]
        public string Ano { get; set; }
        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        [StringLength(500)]
        public string Sinopse { get; set; }
        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(typeof(TimeSpan), "00:01:00", "23:59:59", ErrorMessage = "A duração deve ser entre 1 minuto e 23 horas, 59 minutos e 59 segundos.")]
        public int Duracao { get; set; }

    }
}
