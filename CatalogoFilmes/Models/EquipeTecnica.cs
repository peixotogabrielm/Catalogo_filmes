using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Models
{
    public class EquipeTecnica : Base
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }

        public Filme Filme { get; set; }
        
        public Guid FilmeId { get; set; }

        
    }
}