namespace BaseCleanArchitecture.Domain.Exceptions.ValidationError;

public class ValidationError : IValidationError
{
    public ValidationError(string code, string message)
    {
        Code = code;
        Description = message;
    }

    public string Code { get; set; }
    public string Description { get; set; }
}