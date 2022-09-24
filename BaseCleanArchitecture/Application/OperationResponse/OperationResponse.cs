using System.Text.Json.Serialization;
using BaseCleanArchitecture.Domain.Exceptions;
using BaseCleanArchitecture.Domain.Exceptions.Error;
using BaseCleanArchitecture.Domain.Exceptions.ValidationError;

namespace BaseCleanArchitecture.Application.OperationResponse;

public partial class OperationResponse
{
    public OperationResponse()
    {
        ValidationErrors = new();
    }

    public OperationResponse(IError error) : this()
    {
        Error = error;
    }


    public bool IsSuccess => !IsFailure;
    [JsonIgnore] public bool IsFailure => ValidationErrors.Any() || Error is not null;
    [JsonIgnore] public List<IValidationError> ValidationErrors { get; }
    [JsonIgnore] public IError? Error { get; private set; }

      
     

    public OperationResponse Failure(string message, HttpStatusCode httpStatusCode)
    {
        Error = new Error(message, httpStatusCode);
        return this;
    }

    public OperationResponse Failure(IError error)
    {
        Error = error;
        return this;
    }


    public static OperationResponse Success() => new();


    public static OperationResponse FirstFailureOrSuccess(params OperationResponse[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }
        
    private OperationResponse AddValidationError(IValidationError error)
    {
        ValidationErrors.Add(error);
        return this;
    }

    public static implicit operator OperationResponse(Error error) => new(error);

    public static implicit operator OperationResponse(
        (string message, HttpStatusCode httpStatusCode) messageStatusCode)
        => new(new Error(messageStatusCode.message, messageStatusCode.httpStatusCode));

       
}