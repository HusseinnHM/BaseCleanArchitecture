namespace BaseCleanArchitecture.Domain.Exceptions.Error;

public sealed class ErrorException : Exception, IError
{
    public ErrorException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        StatusCode = httpStatusCode;
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; }
    public new string Message { get; set; }

    public static implicit operator int?(ErrorException? error) => (int?)(error?.StatusCode);
    public static implicit operator HttpStatusCode(ErrorException error) => error?.StatusCode ?? default;
    public static implicit operator string(ErrorException error) => error.Message;

    public override string ToString() => Message;
}
