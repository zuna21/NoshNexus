namespace ApplicationCore.DTOs;

public class Response<T>
{
    public ResponseStatus Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

public enum ResponseStatus
{
    Success,
    BadRequest,
    NotFound,
    UsernameTaken,
    Unauthorized
}
