namespace BaseCleanArchitecture.Domain.Exceptions.ValidationError;

public interface IValidationError
{
    public string Code { get; set; }
    public string Description { get; set; }
}