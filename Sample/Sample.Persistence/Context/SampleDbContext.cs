using BaseCleanArchitecture.Application.Core.Abstractions.Common;
using BaseCleanArchitecture.Application.Dispatchers.DomainEventDispatcher;
using BaseCleanArchitecture.Persistence;
using Microsoft.EntityFrameworkCore;
using Sample.Application.Core.Abstractions.Data;
using Sample.Domain.Entities;

namespace Sample.Persistence.Context;

public sealed class SampleDbContext : BaseDbContext<Guid, User>,ISampleDbContext
{
    public SampleDbContext(DbContextOptions options, IDateTime dateTime, IDomainEventDispatcher domainEventDispatcher) :
        base(options, dateTime, domainEventDispatcher)
    {
    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<ToDoStatus> ToDoStatuses { get; set; }

}