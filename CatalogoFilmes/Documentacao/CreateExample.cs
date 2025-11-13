using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace CatalogoFilmes.Documentacao
{
    public class CreateExample
    {
        public static OpenApiObject ConvertToOpenApiObject(object obj)
        {
            if (obj == null) return new OpenApiObject();

            var openApiObj = new OpenApiObject();
            var properties = obj.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var value = prop.GetValue(obj);

                if (value == null)
                {
                    openApiObj[propName] = new OpenApiString("null");
                }
                else if (value is string str)
                {
                    openApiObj[propName] = new OpenApiString(str);
                }
                else if (value is int i)
                {
                    openApiObj[propName] = new OpenApiInteger(i);
                }
                else if (value is bool b)
                {
                    openApiObj[propName] = new OpenApiBoolean(b);
                }
                else if (value is DateTime dt)
                {
                    openApiObj[propName] = new OpenApiString(dt.ToString("o"));
                }
                else if (value is IEnumerable enumerable && !(value is string))
                {
                    var array = new OpenApiArray();
                    foreach (var item in enumerable)
                    {
                        if (item is string s) array.Add(new OpenApiString(s));
                        else if (item is int x) array.Add(new OpenApiInteger(x));
                        else if (item is bool y) array.Add(new OpenApiBoolean(y));
                        else array.Add(ConvertToOpenApiObject(item)); // recursivo
                    }
                    openApiObj[propName] = array;
                }
                else
                {
                    // recursivo para objetos complexos
                    openApiObj[propName] = ConvertToOpenApiObject(value);
                }
            }

            return openApiObj;
        }
        
    }
}