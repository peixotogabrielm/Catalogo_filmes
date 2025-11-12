using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace CatalogoFilmes.Helpers
{
    public class Successes
    {
        public class OkSuccess : Success
        {
            public OkSuccess(object data)
                : base("OK")
            {
                Metadata.Add("HttpCode", 200);
                Metadata.Add("Data", data);
                Metadata.Add("Timestamp", DateTime.UtcNow); 
            }

            public OkSuccess(object data, string message)
                : base(message)
            {
                Metadata.Add("HttpCode", 200);
                Metadata.Add("Data", data);
                Metadata.Add("Timestamp", DateTime.UtcNow);
            }
            public OkSuccess()
                : base("OK")
            {
                Metadata.Add("HttpCode", 200);
                Metadata.Add("Timestamp", DateTime.UtcNow);
            }
        }

        public class CreatedSuccess : Success
        {
            public CreatedSuccess(string message)
                : base(message)
            {
                Metadata.Add("HttpCode", 201);  
                Metadata.Add("Timestamp", DateTime.UtcNow);
            }
        }

        public class NoContentSuccess : Success
        {
            public NoContentSuccess()
            {
                Metadata.Add("HttpCode", 204);  
                Metadata.Add("Timestamp", DateTime.UtcNow);
            }
        }
        

    }
}