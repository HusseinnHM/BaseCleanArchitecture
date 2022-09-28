using BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;
using Microsoft.AspNetCore.Mvc;

namespace BaseCleanArchitecture.API.Controller;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly IRequestDispatcher Dispatcher;

    public ApiController(IRequestDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }
}