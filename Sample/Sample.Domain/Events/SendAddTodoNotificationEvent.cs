using BaseCleanArchitecture.Domain.Events;
using Sample.Domain.Entities;

namespace Sample.Domain.Events;

public sealed class SendAddTodoNotificationEvent : IDomainEvent
{

    public SendAddTodoNotificationEvent(Notification notification) => Notification = notification;

    public Notification Notification { get; }
}