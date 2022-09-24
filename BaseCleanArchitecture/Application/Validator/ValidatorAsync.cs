using BaseCleanArchitecture.Application.Validator.Base;
using BaseCleanArchitecture.Application.Validator.Interface;
using BaseCleanArchitecture.Domain.Repository;

namespace BaseCleanArchitecture.Application.Validator;

public abstract class ValidatorAsync<TRequest, TRepository> : BaseValidator, IValidator<TRequest, TRepository>
    where TRepository : IRepository

{
    public abstract Task<OperationResponse.OperationResponse> ValidateAsync(TRepository repository, TRequest request);
}