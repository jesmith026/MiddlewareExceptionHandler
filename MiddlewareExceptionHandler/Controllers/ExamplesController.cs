using Microsoft.AspNetCore.Mvc;
using MiddlewareExceptionHandler.Exceptions;
using System;

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

        [HttpGet("Ok")]
        public IActionResult OkExample()
        {
            return Ok();
        }
    }
}