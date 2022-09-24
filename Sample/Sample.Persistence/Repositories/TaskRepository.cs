using BaseCleanArchitecture.Domain.Repository;
using Sample.Application.Core.Abstractions.Data;
using Sample.Domain.Repositories;

namespace Sample.Persistence.Repositories;

public sealed class TaskRepository : Repository<Guid,TaskEntity,ISampleDbContext>, ITaskRepository
{
    public TaskRepository(ISampleDbContext context) : base(context)
    {
    }
}
