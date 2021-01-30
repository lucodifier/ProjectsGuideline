using Guideline.Application.ViewModels;
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
        protected readonly string _requestId;

        public ApiController(IHttpContextAccessor httpContextAccessor, ILogger<TController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestId = Guid.NewGuid().ToString();
            _logger = logger;
        }

        public ApiController(ILogger<TController> logger)
        {
            _requestId = Guid.NewGuid().ToString();
            _logger = logger;
        }

        protected ActionResult CustomResponse<T>(object result = null, string createdUri = null)
        {
            Response.Headers.Add("X-Request-Id", _requestId);

            if (result == null)
                return NoContent();

            if (typeof(T) == typeof(IEnumerable<IViewModel>))
            {
                if (((IEnumerable<IViewModel>)result).Count() == 0)
                {
                    return NotFound(result);
                }
                else
                {
                    return Ok(result);
                }
            }

            if (typeof(T) == typeof(IViewModel))
            {
                return Ok(result);
            }

            if (typeof(T) == typeof(ICreatedViewModel))
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
            Response.Headers.Add("X-Request-Id", _requestId);

            _logger.LogError($"{_requestId} :: {exception.Message} :: {exception.StackTrace}");

            AddError(exception.Message);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", _errors.ToArray() }
            }));
        }

        protected ActionResult CustomValidationResponse(ModelStateDictionary modelState)
        {
            Response.Headers.Add("X-Request-Id", _requestId);

            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }
           
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", _errors.ToArray() }
            }));
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected UserViewModel GetContextUser()
        {
            if (User.FindFirst("Id").Value != null)
            {
                var user = new UserViewModel();
                user.Id = new Guid(User.FindFirst("Id")?.Value);
                user.Login = User.FindFirst("Login")?.Value;
                user.Email = User.FindFirst("Email")?.Value;

                return user;
            }

            return null;
        }
    }
}
