using BaseCleanArchitecture.Domain.Exceptions.ValidationError;

namespace BaseCleanArchitecture.Application.OperationResponse;

public partial class OperationResponse
{
    public OperationResponse SuccessIf<T>(T value, Func<T, bool> predicate, IValidationError error) 
        => predicate(value) ? this : AddValidationError(error);

    public async Task<OperationResponse> SuccessIf<T>(T value, Func<T, Task<bool>> predicate, IValidationError error)
        => await predicate(value) ? this : AddValidationError(error);

    public OperationResponse SuccessIf<T>(T value, Func<T, bool> predicate, string code, string message)
        => SuccessIf(value, predicate, new ValidationError(code, message));


    public async Task<OperationResponse> SuccessIfAsync<T>(T value, Func<T, Task<bool>> predicate, string code, string message) 
        => await SuccessIf(value, predicate, new ValidationError(code, message)); 
    
    public async Task<OperationResponse> SuccessIfAsync<T>(T value, Func<T, Task<bool>> predicate, IValidationError error) 
        => await SuccessIf(value, predicate, error);

}