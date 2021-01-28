using FluentValidation.Results;
using Guideline.Application.Interfaces;
using Guideline.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : ApiController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IHttpContextAccessor httpContextAccessor,
            ILogger<UserController> logger,
            IUserService userService) : base(httpContextAccessor, logger)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria um usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST api/v1/user
        ///     {        
        ///       "name": "Mike",
        ///       "login": "Mike",
        ///       "email": "mike@mail.com",
        ///       "document": "68548478544",
        ///       "pass": "Fsd545$g"
        ///     }
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel request)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                var result = await _userService.Create(request);

                return CustomResponse<ICreatedViewModel>(result, "user");
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                return CustomResponse<IViewModel>(await _userService.GetById(new Guid(id)));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return CustomResponse<IEnumerable<IViewModel>>(await _userService.GetAll());
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }


        [HttpGet, Route("with-documents")]
        public async Task<IActionResult> GetWithDocuments()
        {
            try
            {
                return CustomResponse<IEnumerable<IViewModel>>(await _userService.GetWithDocument());
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        [HttpGet, Route("document/@document")]
        public async Task<IActionResult> GetWithDocuments(string document)
        {
            try
            {
                return CustomResponse<IEnumerable<IViewModel>>(await _userService.GetByDocument(document));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserViewModel request)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                return CustomResponse<ValidationResult>(await _userService.Update(request));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return CustomResponse<Guid>(await _userService.Remove(new Guid(id)));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }
    }
}
