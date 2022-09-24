using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Application.User.Queries.GetAllUser;

namespace Sample.Application.User.Commands.AddUser;

public sealed class AddUserCommand 
{
    public sealed class Request : IRequest<OperationResponse<GetAllUserQuery.Response>>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

}