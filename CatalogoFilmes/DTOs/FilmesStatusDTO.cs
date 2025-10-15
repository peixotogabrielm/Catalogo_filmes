namespace CatalogoFilmes.DTOs
{
    public class FilmesStatusDTO
    {
        public int TotalFilmes { get; set; }
        public string GeneroMaisComum { get; set; }
        public int? AnoMaisRecente { get; set; }
        public int? AnoMaisAntigo { get; set; }
    }
}
