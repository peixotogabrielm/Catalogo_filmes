namespace CatalogoFilmes.Helpers
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Ok(T data) => new ApiResponse<T> { Sucesso = true, Data = data };
        public static ApiResponse<T> Fail(string message) => new ApiResponse<T> { Sucesso = false, Mensagem = message };
    }
}
