using BaseCleanArchitecture.Application.Specification;

namespace Sample.Application.Task.Queries.GetAllTask;

public sealed class GetAllTaskSpecification : Specification<TaskEntity>
{
    public GetAllTaskSpecification(GetAllTaskQuery.Request request)
    {
        ApplyFilter(t =>
            !t.AssignedUserId.HasValue || !request.UserId.HasValue ||
            t.AssignedUserId.Value == request.UserId);
        ApplyOrderByDescending(t => t.Priority);
    }
    
}