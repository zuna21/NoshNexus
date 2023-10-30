namespace API;

public class Response<T>
{
    public ResponseStatus Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
