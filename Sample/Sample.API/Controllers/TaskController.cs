using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
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
    public TaskController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllTaskQuery.Response>))]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTaskQuery.Request request)
        => await Dispatcher
            .SendAsync<GetAllTaskQuery.Request, OperationResponse<IEnumerable<GetAllTaskQuery.Response>>>(request)
            .ToJsonResultAsync();

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetAllTaskQuery.Response))]
    public async Task<IActionResult> Add([FromBody] AddTaskCommand.Requset request)
        => await Dispatcher
            .SendAsync<AddTaskCommand.Requset, OperationResponse<GetAllTaskQuery.Response>>(request)
            .ToJsonResultAsync();
}