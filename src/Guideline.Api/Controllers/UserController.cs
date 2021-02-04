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
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return CustomValidationResponse(ModelState);
                var result = await _userService.CreateAsync(request);

                return CustomResponse<ICreatedResponse>(result, "user");
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }


        /// <summary>
        /// Buscar usuário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return CustomMessageResponse("Id obrigatório");

                return CustomResponse<IResponse>(await _userService.GetByIdAsync(new Guid(id)));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Buscar todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return CustomResponse<IEnumerable<IResponse>>(await _userService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Usuários com documentos
        /// </summary>
        [HttpGet, Route("with-documents")]
        public async Task<IActionResult> GetWithDocuments()
        {
            try
            {
                return CustomResponse<IEnumerable<IResponse>>(await _userService.GetWithDocumentAsync());
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Usuários filtrado pelo documento
        /// </summary>
        [HttpGet, Route("document/@document")]
        public async Task<IActionResult> GetWithDocuments(string document)
        {
            try
            {
                if (string.IsNullOrEmpty(document))
                    return CustomMessageResponse("Documento obrigatório");

                return CustomResponse<IEnumerable<IResponse>>(await _userService.GetByDocumentAsync(document));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Atualizar um usuário
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return CustomValidationResponse(ModelState);

                return CustomResponse<ICreatedResponse>(await _userService.UpdateAsync(request), "user");
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Deletar um usuário
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return CustomResponse<Guid>(await _userService.RemoveAsync(new Guid(id)));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }
    }
}
