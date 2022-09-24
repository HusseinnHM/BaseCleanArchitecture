using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;
using Sample.Domain.Repositories;

namespace Sample.Application.Notification.Queries.GetAllNotification;

public sealed class
    GetAllNotificationHandler : IRequestHandler<GetAllNotificationQuery.Request,
        OperationResponse<IEnumerable<GetAllNotificationQuery.Response>>>
{
    private readonly INotificationRepository _notificationRepository;

    public GetAllNotificationHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<OperationResponse<IEnumerable<GetAllNotificationQuery.Response>>> HandleAsync(GetAllNotificationQuery.Request request, CancellationToken cancellationToken)
    {
        var res = await _notificationRepository.GetAsync(n => n.UserId == request.UserId, GetAllNotificationQuery.Response.Selector);
        return res;
    }
}