using BaseCleanArchitecture.Domain.Repository;
using Sample.Application.Core.Abstractions.Data;
using Sample.Domain.Entities;
using Sample.Domain.Repositories;

namespace Sample.Persistence.Repositories;

public sealed class UserRepository : Repository<Guid, User,ISampleDbContext>, IUserRepository
{
    public UserRepository(ISampleDbContext context) : base(context)
    {
    }
}