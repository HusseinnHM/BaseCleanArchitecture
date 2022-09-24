using BaseCleanArchitecture.Application.Core.Abstractions.Data;

namespace Sample.Application.Core.Abstractions.Data;

public interface ISampleDbContext : IDbContext<Guid>
{
    
}