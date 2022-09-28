using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Todo.Commands.AddTodo;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class ToDoController : ApiController
{
    public ToDoController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }


    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OperationResponse))]
    public async Task<IActionResult> Add([FromBody] AddTodoCommand.Request request)
        => await Dispatcher
            .SendAsync<AddTodoCommand.Request, OperationResponse>
            (request).ToJsonResultAsync();
}