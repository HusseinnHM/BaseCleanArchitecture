using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Task.Commands.AddTask;
using Sample.Application.Task.Queries.GetAllTask;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class TaskController : ApiController
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllTaskQuery.Response>))]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllTaskQuery.Request, OperationResponse<IEnumerable<GetAllTaskQuery.Response>>> handler,
        [FromQuery] GetAllTaskQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetAllTaskQuery.Response))]
    public async Task<IActionResult> Add(
        [FromServices] IRequestHandler<AddTaskCommand.Requset, OperationResponse<GetAllTaskQuery.Response>> handler,
        [FromBody] AddTaskCommand.Requset request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}