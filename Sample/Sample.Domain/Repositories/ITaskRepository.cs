using BaseCleanArchitecture.Domain.Repository;

namespace Sample.Domain.Repositories;

public interface ITaskRepository : IRepository<Guid,TaskEntity>
{
    // extra methods
}

