using BaseCleanArchitecture.API.Controller;
using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using BaseCleanArchitecture.Domain.Exceptions;
using BaseCleanArchitecture.Domain.Exceptions.Error;
using BaseCleanArchitecture.Domain.Exceptions.ValidationError;
using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class ZTestController : ApiController
{
    public ZTestController(IRequestDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    public IActionResult TestThrowErrorException()
    {
        throw new ErrorException("Test Throw ErrorException", HttpStatusCode.Exception500);
    }

    [HttpGet]
    public IActionResult TestThrowValidationException()
    {
        throw new ValidationException("Test", "Test Throw ValidationException");
    }

    [HttpGet]
    public IActionResult TestThrowException()
    {
        throw new ApplicationException("This test");
    }
}