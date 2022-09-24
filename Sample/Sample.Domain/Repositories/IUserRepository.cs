using BaseCleanArchitecture.Domain.Repository;
using Sample.Domain.Entities;

namespace Sample.Domain.Repositories;

public interface IUserRepository : IRepository<Guid, User>
{
    // extra methods
}