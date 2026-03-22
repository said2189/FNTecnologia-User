namespace Application.Common
{
    public class Response<T>
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }
        public T? Value { get; private set; }
        private Response(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }
        public static Response<T> Success(T value) => new Response<T>(true, value, null);
        public static Response<T> Failure(string error) => new Response<T>(false, default, error);
    }

}
