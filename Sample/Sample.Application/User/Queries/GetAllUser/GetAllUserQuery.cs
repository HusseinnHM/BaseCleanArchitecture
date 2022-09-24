using System.Linq.Expressions;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;

namespace Sample.Application.User.Queries.GetAllUser;

public sealed class GetAllUserQuery
{
    public sealed class Request : IRequest<OperationResponse<IEnumerable<Response>>>
    {
        
    }
    public sealed class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Domain.Entities.User, Response>> Selector => u
            => new()
            {
                Id = u.Id,
                Name = u.Name
            };
    }
}