using Guideline.Application.ViewModels;
using Guideline.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guideline.Api.Controllers
{
    [Authorize]
    public abstract class ApiController<TController> : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected readonly ILogger<TController> _logger;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ApiController(IHttpContextAccessor httpContextAccessor, ILogger<TController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public ApiController(ILogger<TController> logger)
        {
            _logger = logger;
        }

        protected ActionResult CustomResponse<T>(object result = null, string createdUri = null)
        {
            if (!Response.Headers.ContainsKey("X-Request-Id"))
                Response.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());

            if (result == null)
                return NoContent();

            if (typeof(T) == typeof(IEnumerable<IResponse>))
            {
                if (((IEnumerable<IResponse>)result).Count() == 0)
                {
                    return NotFound(result);
                }
                else
                {
                    return Ok(result);
                }
            }

            if (typeof(T) == typeof(IResponse))
            {
                return Ok(result);
            }

            if (typeof(T) == typeof(ICreatedResponse))
            {
                return Created(createdUri, result);
            }

            if (typeof(T) == typeof(Guid))
            {
                return Ok(result);
            }

            return NoContent();
        }

        protected ActionResult CustomExceptionResponse(Exception exception)
        {
            var requestId = Guid.NewGuid().ToString();
            if (!Response.Headers.ContainsKey("X-Request-Id"))
                Response.Headers.Add("X-Request-Id", requestId);

            _logger.LogError($"{requestId} :: {exception.Message} :: {exception.StackTrace}");

            AddError(exception.Message);

            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", _errors.ToArray() }
            });
            problemDetails.Title = ApplicationMessages.RESPONSE_ERROR;

            return BadRequest(problemDetails);
        }

        protected ActionResult CustomMessageResponse(string message)
        {
            var requestId = Guid.NewGuid().ToString();
            if (!Response.Headers.ContainsKey("X-Request-Id"))
                Response.Headers.Add("X-Request-Id", requestId);

            AddError(message);

            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", _errors.ToArray() }
            });
            problemDetails.Title = ApplicationMessages.VALIDATIONS_TITLE;

            return BadRequest(problemDetails);
        }

        protected ActionResult CustomValidationResponse(ModelStateDictionary modelState)
        {
            if (!Response.Headers.ContainsKey("X-Request-Id"))
                Response.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());

            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", _errors.ToArray() }
            });
            problemDetails.Title = ApplicationMessages.VALIDATIONS_TITLE;

            return BadRequest(problemDetails);
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected UserResponse GetContextUser()
        {
            if (User.FindFirst("Id").Value != null)
            {
                var user = new UserResponse();
                user.Id = new Guid(User.FindFirst("Id")?.Value);
                user.Login = User.FindFirst("Login")?.Value;
                user.Email = User.FindFirst("Email")?.Value;

                return user;
            }

            return null;
        }
    }
}
