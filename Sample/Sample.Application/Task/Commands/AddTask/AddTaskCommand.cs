using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Application.Task.Queries.GetAllTask;

namespace Sample.Application.Task.Commands.AddTask;

public sealed class AddTaskCommand
{
    public sealed class Requset : IRequest<OperationResponse<GetAllTaskQuery.Response>>
    {
        public string Text { get; set; }
        public int Priority { get;  set; }
        public Guid? AssignedUserId { get; set;}    
    }
    
}