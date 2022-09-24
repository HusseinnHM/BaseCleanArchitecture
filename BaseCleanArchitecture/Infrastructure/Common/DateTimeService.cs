using BaseCleanArchitecture.Application.Core.Abstractions.Common;

namespace BaseCleanArchitecture.Infrastructure.Common;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now  => DateTime.Now;
}