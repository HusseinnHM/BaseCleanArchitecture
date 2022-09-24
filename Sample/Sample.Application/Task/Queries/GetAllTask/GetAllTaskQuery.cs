using System.Linq.Expressions;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;

namespace Sample.Application.Task.Queries.GetAllTask;

public sealed class GetAllTaskQuery
{
    public sealed class Request : IRequest<OperationResponse<IEnumerable<Response>>>
    {
        public Guid? UserId { get; set; }
    }


    public sealed class Response
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime DatCreated { get; set; }

        public static Expression<Func<TaskEntity, Response>> Selector => t
            => new()
            {
                Id = t.Id,
                Text = t.Text,
                UserId = t.AssignedUserId,
                UserName = t.AssignedUser.Name ?? null,
                DatCreated = t.DateCreated
            };
    }
}