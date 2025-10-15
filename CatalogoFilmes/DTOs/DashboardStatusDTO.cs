namespace CatalogoFilmes.DTOs
{
    public class DashboardStatusDTO
    {
        public int TotalFilmes { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalAdmins { get; set; }
        public Dictionary<string, int> FilmesPorGenero { get; set; }
        public Dictionary<int, int> FilmesPorAno { get; set; }
    }
}
