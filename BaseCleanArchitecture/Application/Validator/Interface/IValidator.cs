using BaseCleanArchitecture.Domain.Repository;

namespace BaseCleanArchitecture.Application.Validator.Interface;

public interface IValidator
{
}

public interface IValidator<in TRequest> : IValidator
{
    OperationResponse.OperationResponse Validate(TRequest request);
}

public interface IValidator<in TRequest, in TRepository> : IValidator
    where TRepository : IRepository
{
    Task<OperationResponse.OperationResponse> ValidateAsync(TRepository repository, TRequest request);
}