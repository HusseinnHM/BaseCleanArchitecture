using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Domain.Repositories;

namespace Sample.Application.User.Queries.GetAllUser;

public sealed class GetAllUserHandler : IRequestHandler<GetAllUserQuery.Request,OperationResponse<IEnumerable<GetAllUserQuery.Response>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResponse<IEnumerable<GetAllUserQuery.Response>>> HandleAsync(GetAllUserQuery.Request request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAsync(GetAllUserQuery.Response.Selector);
    }
}