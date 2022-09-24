using BaseCleanArchitecture.Domain.Events;
using Sample.Domain.Events;
using Sample.Domain.Repositories;

namespace Sample.Application.Notification.Events.SendAddTodoNotification;

public sealed class SendAddTodoNotificationEventHandler : IDomainEventHandler<SendAddTodoNotificationEvent>
{
    private readonly INotificationRepository _notificationRepository;
        

    public SendAddTodoNotificationEventHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
            
    }

    public async TaskThread HandleAsync(SendAddTodoNotificationEvent domainEvent, CancellationToken cancellationToken)
    {
        _notificationRepository.Add(domainEvent.Notification);
        await _notificationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}