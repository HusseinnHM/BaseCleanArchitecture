using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Notification.Queries.GetAllNotification;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class NotificationController : ApiController
{
    public NotificationController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }
    
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllNotificationQuery.Response>))]
    public async Task<IActionResult> GetAll([FromQuery] GetAllNotificationQuery.Request request)
        => await Dispatcher
            .SendAsync<GetAllNotificationQuery.Request, OperationResponse<IEnumerable<GetAllNotificationQuery.Response>>>(request)
            .ToJsonResultAsync();

  
}