using BaseCleanArchitecture.Domain.Repository;
using Sample.Domain.Entities;

namespace Sample.Domain.Repositories;

public interface INotificationRepository : IRepository<Guid,Notification>
{
    // extra methods
}