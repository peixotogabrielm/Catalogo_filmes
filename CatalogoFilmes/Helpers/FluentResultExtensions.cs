using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using static CatalogoFilmes.Helpers.Errors;
using static CatalogoFilmes.Helpers.Successes;

namespace CatalogoFilmes.Helpers
{
    public static class FluentResultExtensions
    {
        public static IResult ToApiResult<T>(this Result<T> result)
        {
            var firstSuccess = result.Successes.FirstOrDefault();
            if (firstSuccess != null)
            {

                if (firstSuccess is NoContentSuccess)
                    return TypedResults.NoContent();

                var data = firstSuccess.Metadata.ContainsKey("Data") 
                    ? firstSuccess.Metadata["Data"] 
                    : result.Value;

                if (firstSuccess is OkSuccess)
                    return TypedResults.Ok(data);

                if (firstSuccess is CreatedSuccess)
                    return TypedResults.Created("", data);
            }

            // Aqui você pode customizar conforme o tipo de erro
            var firstError = result.Errors.FirstOrDefault();

            if (firstError == null)
                return TypedResults.BadRequest("Erro desconhecido.");

            if (firstError is NotFoundError)
                return TypedResults.NotFound(firstError.Message);

            if (firstError is BadRequestError)
                return TypedResults.BadRequest(firstError.Message);

            // fallback genérico
            return TypedResults.BadRequest(firstError.Message);
        }
    }
}