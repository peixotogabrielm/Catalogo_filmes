using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Models
{
    public class Roles : Base
    {
        public string Role { get; set; } = "User";
        public ICollection<Usuario> Usuarios { get; set; }
    }
}