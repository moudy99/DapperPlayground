namespace Dapper_VS_EFcore
{
    public class GlobalResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = false;
        public T Data { get; set; }
    }
}
