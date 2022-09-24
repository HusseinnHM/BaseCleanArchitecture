using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
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
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllUserQuery.Response>))]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllUserQuery.Request, OperationResponse<IEnumerable<GetAllUserQuery.Response>>> handler,
        [FromQuery] GetAllUserQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetAllUserQuery.Response))]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddUserCommand.Request, OperationResponse<GetAllUserQuery.Response>> handler,
        [FromBody] AddUserCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}