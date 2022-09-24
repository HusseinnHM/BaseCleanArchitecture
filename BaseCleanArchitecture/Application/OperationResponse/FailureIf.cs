using BaseCleanArchitecture.Domain.Exceptions.ValidationError;

namespace BaseCleanArchitecture.Application.OperationResponse;

public  partial class OperationResponse
{
    public OperationResponse FailureIf<T>(T value, Func<T, bool> predicate, IValidationError error) =>
        predicate(value) ? AddValidationError(error) : this;

    public async Task<OperationResponse>
        FailureIf<T>(T value, Func<T, Task<bool>> predicate, IValidationError error) =>
        await predicate(value) ? AddValidationError(error) : this;

    public OperationResponse FailureIf<T>(T value, Func<T, bool> predicate, string code, string message) =>
        FailureIf(value, predicate, new ValidationError(code, message));


    public async Task<OperationResponse> FailureIfAsync<T>(T value, Func<T, Task<bool>> predicate, string code,
        string message) =>
        await FailureIf(value, predicate, new ValidationError(code, message));

}