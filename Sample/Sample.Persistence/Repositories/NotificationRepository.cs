using BaseCleanArchitecture.Domain.Repository;
using Sample.Application.Core.Abstractions.Data;
using Sample.Domain.Entities;
using Sample.Domain.Repositories;

namespace Sample.Persistence.Repositories;

public sealed class NotificationRepository : Repository<Guid,Notification,ISampleDbContext>, INotificationRepository
{
   
    public NotificationRepository(ISampleDbContext context) : base(context)
    {
    }
}