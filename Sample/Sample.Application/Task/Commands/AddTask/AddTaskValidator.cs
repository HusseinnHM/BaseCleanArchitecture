using BaseCleanArchitecture.Application.OperationResponse;
using BaseCleanArchitecture.Application.Validator;

namespace Sample.Application.Task.Commands.AddTask;

public sealed class AddTaskValidator : Validator<AddTaskCommand.Requset>
{
    public override OperationResponse Validate(AddTaskCommand.Requset request)
    {
        RoleFor(request.Priority, 
            p => p > 0,
            new (nameof(request.Priority),"Must be grater than 0"));

        return OperationResponse;
    }
}