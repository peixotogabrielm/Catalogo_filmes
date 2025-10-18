namespace CatalogoFilmes.Helpers
{
    public class Result<T>
    {
        public int StatusCode { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Data { get; set; }

        
        public static Result<T> Ok(int statusCode,T data) => new Result<T> { StatusCode = statusCode, Sucesso = true, Data = data };
        public static Result<T> Fail(int statusCode, string message) => new Result<T> { StatusCode = statusCode, Sucesso = false, Mensagem = message };
    }
}
