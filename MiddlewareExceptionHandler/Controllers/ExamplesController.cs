using Microsoft.AspNetCore.Mvc;
using MiddlewareExceptionHandler.Exceptions;
using System;
using System.Security.Authentication;

namespace MiddlewareExceptionHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        [HttpGet("NotFound")]
        public IActionResult NotFoundExample()
        {
            throw new NotFoundException("Testing a Not Found Exception.");
        }

        [HttpGet("NotImplemented")]
        public IActionResult NotImplementedExample()
        {
            throw new NotImplementedException("Testing a Not Implemented Exception");
        }

        [HttpGet("BadRequest")]
        public IActionResult BadRequestExample()
        {
            throw new BadRequestException("Testing a Bad Request Exception");
        }

        [HttpGet("NotAuthorized")]
        public IActionResult NotAuthorizedExample()
        {
            throw new AuthenticationException("Testing a Not Authorized Exception");
        }

        [HttpGet("Ok")]
        public IActionResult OkExample()
        {
            return Ok();
        }
    }
}