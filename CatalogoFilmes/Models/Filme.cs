using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmes.Models
{
    public class Filme : Base
    {

        public string Titulo { get; set; }

        public string Ano { get; set; }

        public string Genero { get; set; }

        public string Sinopse { get; set; }

        public string Tipo { get; set; }

        public int Duracao { get; set; }

        public string ImdbId { get; set; }

        public string Idioma { get; set; }

        public string Poster { get; set; }

        public ICollection<Classificacoes> Classificacoes { get; set; } = new List<Classificacoes>();

        public ICollection<EquipeTecnica> Equipes { get; set; } = new List<EquipeTecnica>();
        
        public ICollection<Catalogo> Catalogos { get; set; }

    }
    

}
