using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplicationAssesment.Common.Models;

namespace WebApplicationAssesment.Common
{
    public class BaseController: ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;
        private readonly string? _successDefaultMessage = null;
        private readonly string? _badRequestDefaultMessage = null;

        public BaseController(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
            
        protected IActionResult SuccessResponse(string message, object? data = null)
        {
            message ??= "Successful Request";
            return Ok(new ResponseBaseModel
            {
                Code = HttpStatusCode.OK,
                Message = message,
                Data = data
            });
        }

        protected IActionResult SuccessResponse()
        {
            return Ok(new ResponseBaseModel
            {
                Code = HttpStatusCode.OK,
                Message = _successDefaultMessage
            });
        }

        protected IActionResult ErrorResponse(string message)
        {
            return BadRequest(new ResponseBaseModel()
            {
                Code = HttpStatusCode.InternalServerError,
                Message = message
            });
        }


        protected IActionResult ErrorResponse(HttpStatusCode code, object data)
        {
            return BadRequest(new ResponseBaseModel
            {
                Code = code,
                Message = this._badRequestDefaultMessage,
                Data = data
            });
        }

        protected IActionResult ErrorResponse()
        {
            return BadRequest(new ResponseBaseModel
            {
                Code = HttpStatusCode.InternalServerError,
                Message = _badRequestDefaultMessage
            });
        }

        protected IActionResult BadRequest(HttpStatusCode code, object data)
        {
            return BadRequest(new ResponseBaseModel
            {
                Code = code,
                Message = _badRequestDefaultMessage,
                Data = data
            });
        }

        protected IActionResult BadRequest(HttpStatusCode code, string message, object data)
        {
            return BadRequest(new ResponseBaseModel
            {
                Code = code,
                Message = message,
                Data = data
            });
        }

        protected IActionResult BadRequest(string message, object? data = null)
        {
            return BadRequest(new ResponseBaseModel
            {
                Code = HttpStatusCode.BadRequest,
                Message = message,
                Data = data
            });
        }


        protected IActionResult NotFound(string message)
        {
            return NotFound(new ResponseBaseModel()
            {
                Code = HttpStatusCode.NotFound,
                Message = message,
                Data = null
            });
        }
    }
}

