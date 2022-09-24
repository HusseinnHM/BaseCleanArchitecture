using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;

namespace Sample.Application.Todo.Commands.AddTodo;

public sealed class AddTodoCommand
{
    public sealed class Request : IRequest<OperationResponse>
    {
        public Guid TaskId { get; set; }
        public string Text { get; set; }
    }
}