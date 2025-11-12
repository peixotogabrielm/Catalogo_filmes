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
                var data = firstSuccess.Metadata.ContainsKey("Data")
                    ? firstSuccess.Metadata["Data"]
                    : result.Value;

                if (firstSuccess is OkSuccess)
                    return TypedResults.Ok(data);

                if (firstSuccess is CreatedSuccess)
                    return TypedResults.Created("", data);
            }

            var firstError = result.Errors.FirstOrDefault();

            if (firstError == null)
                return TypedResults.BadRequest("Erro desconhecido.");

            if (firstError is NotFoundError)
                return TypedResults.NotFound(firstError.Message);

            if (firstError is BadRequestError)
                return TypedResults.BadRequest(firstError.Message);

            return TypedResults.BadRequest(firstError.Message);
        }

        public static IResult ToApiResult(this Result result)
        {
            var converted = Result.Ok();
            converted.Reasons.AddRange(result.Reasons);
            return converted.ToApiResult();

        }
    }
}