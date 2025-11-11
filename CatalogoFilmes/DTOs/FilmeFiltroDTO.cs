namespace CatalogoFilmes.DTOs
{
    public class FilmeFiltroDTO
    {
        public string? Titulo { get; set; }
        public List<string>? Genero { get; set; }
        public string? Ano { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
