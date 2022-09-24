
namespace BaseCleanArchitecture.Application.Core.Abstractions.Common;

public interface IDateTime
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
}