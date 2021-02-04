using Guideline.Application.Interfaces;
using Guideline.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Guideline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ApiController<TokenController>
    {
        private readonly ITokenService _tokenService;

        public TokenController(IHttpContextAccessor httpContextAccessor,
           ILogger<TokenController> logger,
           ITokenService tokenService) : base(httpContextAccessor, logger)
        {
            _tokenService = tokenService;            
        }


        /// <summary>
        /// Gerar token a partir de usuário e senha
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromHeader] string login, [FromHeader] string pass)
        {
            try
            {
                return CustomResponse<IResponse>(await _tokenService.GenerateTokenAsync(login, pass));
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse(ex);
            }
        }

    }
}
