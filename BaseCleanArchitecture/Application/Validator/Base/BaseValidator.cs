using BaseCleanArchitecture.Domain.Exceptions.ValidationError;

namespace BaseCleanArchitecture.Application.Validator.Base;

public class BaseValidator
{
    protected readonly OperationResponse.OperationResponse OperationResponse;

    protected BaseValidator()
    {
        OperationResponse = new();
    }

    protected void RoleFor<TProperty>(TProperty property, Func<TProperty, bool> predicate, IValidationError error)
        => OperationResponse.SuccessIf(property, predicate, error);

    protected void RoleFor<TProperty>(TProperty property, Func<TProperty, bool> predicate, ValidationError error)
        => OperationResponse.SuccessIf(property, predicate, error);

    protected async Task RoleForAsync<TProperty>(TProperty property, Func<TProperty, Task<bool>> predicate, IValidationError error)
        => await OperationResponse.SuccessIfAsync(property, predicate, error);

    protected async Task RoleForAsync<TProperty>(TProperty property, Func<TProperty, Task<bool>> predicate, ValidationError error)
        => await OperationResponse.SuccessIfAsync(property, predicate, error);
}