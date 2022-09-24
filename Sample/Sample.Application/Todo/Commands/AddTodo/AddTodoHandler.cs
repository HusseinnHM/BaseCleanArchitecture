using BaseCleanArchitecture.Application.Core.Abstractions.Data;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Domain.Repositories;

namespace Sample.Application.Todo.Commands.AddTodo;

public sealed class AddTodoHandler : IRequestHandler<AddTodoCommand.Request, OperationResponse>
{
    private readonly ITaskRepository _taskRepository;
    


    public AddTodoHandler(IUnitOfWork unitOfWork, ITaskRepository taskRepository)
    {
        
        _taskRepository = taskRepository;
    }

    public async Task<OperationResponse> HandleAsync(AddTodoCommand.Request request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetAsync(request.TaskId,"Todos");
        task.AddToDo(request.Text);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResponse.Success();
    }
}