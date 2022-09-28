using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.User.Commands.AddUser;
using Sample.Application.User.Queries.GetAllUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class UserController : ApiController
{
    public UserController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }
    
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllUserQuery.Response>))]
    public async Task<IActionResult> GetAll()
        => await Dispatcher
            .SendAsync<GetAllUserQuery.Request, OperationResponse<IEnumerable<GetAllUserQuery.Response>>>
            (new ())
            .ToJsonResultAsync();

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetAllUserQuery.Response))]
    public async Task<IActionResult> Add([FromBody] AddUserCommand.Request request)
        => await Dispatcher
            .SendAsync<AddUserCommand.Request, OperationResponse<GetAllUserQuery.Response>>(request)
            .ToJsonResultAsync();

   
}