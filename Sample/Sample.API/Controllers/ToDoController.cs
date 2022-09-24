using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Todo.Commands.AddTodo;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class ToDoController : ApiController
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddTodoCommand.Request, OperationResponse> handler,
        [FromBody] AddTodoCommand.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}