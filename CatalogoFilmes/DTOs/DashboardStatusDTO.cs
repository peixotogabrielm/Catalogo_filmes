namespace CatalogoFilmes.DTOs
{
    public class DashboardStatusDTO
    {
        public int TotalFilmes { get; set; }
        public int TotalUsuarios { get; set; }
        public Dictionary<string, int> FilmesPorGenero { get; set; }
        public Dictionary<string, int> FilmesPorAno { get; set; }
    }
}
