using BaseCleanArchitecture.Application.Validator.Base;
using BaseCleanArchitecture.Application.Validator.Interface;

namespace BaseCleanArchitecture.Application.Validator;

public abstract class Validator<TRequest> : BaseValidator, IValidator<TRequest>

{
    public abstract OperationResponse.OperationResponse Validate(TRequest request);
}