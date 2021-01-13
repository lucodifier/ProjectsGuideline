using Guideline.Application.Interfaces;
using Guideline.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Guideline.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost, Route("v1/[controller]/register")]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel request)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Create(request));
        }

        [AllowAnonymous]
        [HttpPost, Route("v1/[controller]/login")]
        public async Task<IActionResult> Post([FromBody] LoginUserViewModel request)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Get(request.Login, request.Pass));
        }

        [AllowAnonymous]
        [HttpGet, Route("v1/[controller]/:id")]
        public async Task<IActionResult> GetById(string id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.GetById(new Guid(id)));
        }

        [AllowAnonymous]
        [HttpGet, Route("v1/[controller]")]
        public async Task<IActionResult> Get()
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.GetAll());
        }

        [AllowAnonymous]
        [HttpPut, Route("v1/[controller]")]
        public async Task<IActionResult> Put([FromBody] UpdateUserViewModel request)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Update(request));
        }

        [AllowAnonymous]
        [HttpDelete, Route("v1/[controller]")]
        public async Task<IActionResult> Delete(string id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Remove(new Guid(id)));
        }
    }
}
