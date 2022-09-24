namespace BaseCleanArchitecture.Application.Core.Abstractions.Request;

public interface IRequest
{
}
    
public interface IRequest<TResponse> : IRequest
{
}