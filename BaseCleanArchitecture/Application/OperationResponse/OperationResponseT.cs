using BaseCleanArchitecture.Domain.Exceptions;
using BaseCleanArchitecture.Domain.Exceptions.Error;
using BaseCleanArchitecture.Domain.Exceptions.ValidationError;

namespace BaseCleanArchitecture.Application.OperationResponse;

public class OperationResponse<TResponse> : OperationResponse
{
    public OperationResponse()
    {
    }

    public OperationResponse(TResponse response) => Response = response;

    public OperationResponse(TResponse response, IError error) : base(error)
        => Response = response;

    public OperationResponse(IError error) : base(error)
    {
    }

    public TResponse? Response { get; set; }


    public new OperationResponse<TResponse> Validate<T>(T value, Func<T, bool> predicate, IValidationError error) =>
        predicate(value) ? AddValidationFailure(error) : this;

    public new async Task<OperationResponse<TResponse>>
        Validate<T>(T value, Func<T, Task<bool>> predicate, IValidationError error) =>
        await predicate(value) ? AddValidationFailure(error) : this;

    public new OperationResponse<TResponse>
        Validate<T>(T value, Func<T, bool> predicate, string code, string message) =>
        Validate(value, predicate, new ValidationError(code, message));

    public new async Task<OperationResponse<TResponse>> ValidateAsync<T>(T value, Func<T, Task<bool>> predicate, string code,
        string message) =>
        await Validate(value, predicate, new ValidationError(code, message));


    public new OperationResponse<TResponse> Failure(string message, HttpStatusCode httpStatusCode)
    {
        Failure(new Error(message, httpStatusCode)) ;
        return this;
    }

    public new OperationResponse<TResponse> Failure(IError error)
    {
        base.Failure(error) ;
        return this;
    }

    public static OperationResponse<TResponse> Create() => new();

    public static OperationResponse<TResponse> Create(TResponse value, Error error)
        => new(value, error);

    public static implicit operator OperationResponse<TResponse>(TResponse response)
        => new (response);

    public static implicit operator OperationResponse<TResponse>(Error error)
        => new (error);   
        
    public static implicit operator OperationResponse<TResponse>((OperationResponse operationResponse,string? _) operationResponse)
    {
        var response = new OperationResponse<TResponse>(operationResponse.operationResponse.Error!);
        operationResponse.operationResponse.ValidationErrors.ForEach(e => response.AddValidationFailure(e));
        return response;
    }

    private OperationResponse<TResponse> AddValidationFailure(IValidationError error)
    {
        ValidationErrors.Add(error);
        return this;
    }
}