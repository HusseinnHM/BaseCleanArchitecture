using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Application.Task.Queries.GetAllTask;
using Sample.Domain.Repositories;

namespace Sample.Application.Task.Commands.AddTask;

public sealed class AddTaskHandler : IRequestHandler<AddTaskCommand.Requset, OperationResponse<GetAllTaskQuery.Response>>
{
    private readonly ITaskRepository _taskRepository;


    public AddTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<OperationResponse<GetAllTaskQuery.Response>> HandleAsync(AddTaskCommand.Requset request, CancellationToken cancellationToken)
    {
        var response = new AddTaskValidator().Validate(request);
        if (response.IsFailure) return (response, null);
        var task = new TaskEntity(request.Text, request.Priority, request.AssignedUserId);
        _taskRepository.Add(task);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return await _taskRepository.GetAsync(task.Id, GetAllTaskQuery.Response.Selector);
    }
}