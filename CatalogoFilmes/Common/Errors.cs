using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace CatalogoFilmes.Helpers
{
    public class Errors
    {
        public class UnauthorizedError : Error
        {
            public UnauthorizedError(string message) 
                : base(message)
            {
                // Se você quiser adicionar metadados específicos do 401:
                Metadata.Add("HttpCode", 401);
            }
        }

        // Erro para recursos não encontrados (Status 404)
        public class NotFoundError : Error
        {
            public NotFoundError(string message) 
                : base(message)
            {
                Metadata.Add("HttpCode", 404);
            }
        }

        // Erro para falhas de validação de regra de negócio (Status 400 - Opcional, pois o default é 400)
        public class BadRequestError : Error
        {
            public BadRequestError(string message) 
                : base(message)
            {
                Metadata.Add("HttpCode", 400);
            }
        }
    }
}