using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Models
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}