using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmes.Helpers
{
    public class EnumHelper
    {
        public async static Task<List<object>> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                    .Cast<T>()
                    .Select(e => new
                    {
                        Id = (int)(object)e,   // valor numérico
                        Name = e.ToString()     // nome da enum
                    })
                    .ToList<object>();
        }
    }
}