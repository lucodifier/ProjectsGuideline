using Guideline.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
           IConfiguration config,
           ITokenService tokenService) : base(httpContextAccessor, logger)
        {
            _tokenService = tokenService;
            _tokenService.Configuration = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromHeader] string login, [FromHeader] string pass)
        {
            var result = await _tokenService.GenerateTokenAsync(login, pass);
            return Ok(result);
        }

    }
}
