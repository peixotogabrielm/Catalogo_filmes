using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Models
{
       public class Classificacoes : Base
    {
        public string Fonte { get; set; }
        public string Nota { get; set; }
        public Guid FilmeId { get; set; }
        public Filme Filme { get; set; }
    }
}