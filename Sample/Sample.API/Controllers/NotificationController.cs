using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Notification.Queries.GetAllNotification;
using Swashbuckle.AspNetCore.Annotations;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class NotificationController : ApiController
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<GetAllNotificationQuery.Response>))]
    public async Task<IActionResult> GetAll(
        [FromServices] IRequestHandler<GetAllNotificationQuery.Request, OperationResponse<IEnumerable<GetAllNotificationQuery.Response>>> handler,
        [FromQuery] GetAllNotificationQuery.Request request)
        => await handler.HandleAsync(request).ToJsonResultAsync();
}