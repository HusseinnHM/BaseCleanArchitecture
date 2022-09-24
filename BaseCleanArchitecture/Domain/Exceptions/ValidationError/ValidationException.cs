namespace BaseCleanArchitecture.Domain.Exceptions.ValidationError;

public class ValidationException : Exception, IValidationError
{
    public ValidationException(string code, string message) : base($"{code} : {message}")
    {
        Code = code;
        Description = message;
    }

    public string Code { get; set; }
    public string Description { get; set; }
}