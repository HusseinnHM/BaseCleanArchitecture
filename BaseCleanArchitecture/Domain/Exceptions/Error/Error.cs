namespace BaseCleanArchitecture.Domain.Exceptions.Error;

public class Error : IError
{
    public Error(string message,HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}