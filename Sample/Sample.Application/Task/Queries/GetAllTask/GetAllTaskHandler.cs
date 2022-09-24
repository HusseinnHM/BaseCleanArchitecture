using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Domain.Repositories;

namespace Sample.Application.Task.Queries.GetAllTask;

public sealed class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery.Request, OperationResponse<IEnumerable<GetAllTaskQuery.Response>>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<OperationResponse<IEnumerable<GetAllTaskQuery.Response>>> HandleAsync(GetAllTaskQuery.Request request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAsync(new GetAllTaskSpecification(request), GetAllTaskQuery.Response.Selector);
    }
}