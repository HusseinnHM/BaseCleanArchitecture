using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Application.User.Queries.GetAllUser;
using Sample.Domain.Repositories;

namespace Sample.Application.User.Commands.AddUser;

public sealed class AddUserHandler : IRequestHandler<AddUserCommand.Request,OperationResponse<GetAllUserQuery.Response>>
{
    private readonly IUserRepository _userRepository;
    

    public AddUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
    }

    public async Task<OperationResponse<GetAllUserQuery.Response>> HandleAsync(AddUserCommand.Request request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User(request.Name, request.UserName, request.Email);
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _userRepository.GetAsync(user.Id, GetAllUserQuery.Response.Selector);
    }
}