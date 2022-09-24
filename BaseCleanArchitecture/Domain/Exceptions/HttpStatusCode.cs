using Microsoft.AspNetCore.Http;

namespace BaseCleanArchitecture.Domain.Exceptions;

public enum HttpStatusCode:uint
{
    /// <summary>
    /// As <see cref="StatusCodes.Status200OK"/>
    /// </summary>
    Ok200 = 200,
    /// <summary>
    /// As <see cref="StatusCodes.Status202Accepted"/>
    /// </summary>
    Exist202 = 202,
    /// <summary>
    /// As <see cref="StatusCodes.Status404NotFound"/>
    /// </summary>
    NotExist404 = 404,
    /// <summary>
    /// As <see cref="StatusCodes.Status400BadRequest"/>
    /// </summary>
    Failed400 = 400,
    /// <summary>
    /// As <see cref="StatusCodes.Status403Forbidden"/>
    /// </summary>
    Forbidden403 = 403,
    /// <summary>
    /// As <see cref="StatusCodes.Status500InternalServerError"/>
    /// </summary>
    Exception500 = 500,
    /// <summary>
    /// Useful in third party api
    /// As <see cref="StatusCodes.Status401Unauthorized"/>
    /// </summary>
    Unauthorized401 = 401,
}