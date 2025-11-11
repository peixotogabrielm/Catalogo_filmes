namespace CatalogoFilmes.DTOs
{
    public class FilmesStatusDTO
    {
        public int TotalFilmes { get; set; }
        public string GeneroMaisComum { get; set; }
        public string? AnoMaisRecente { get; set; }
        public string? AnoMaisAntigo { get; set; }
    }
}
