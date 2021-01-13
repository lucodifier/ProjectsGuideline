using AutoMapper;
using Guideline.Application.Interfaces;
using Guideline.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guideline.Api.Midlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IMapper mapper)
        {
            _next = next;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, userService, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var _secret = _configuration.GetSection("JwtSettings").GetSection("secret").Value;
                var _key = Encoding.ASCII.GetBytes(_secret);
                var _expDate = DateTime.UtcNow.AddMinutes(double.Parse(_configuration.GetSection("JwtSettings").GetSection("expiration").Value));
                var _audience = _configuration.GetSection("JwtSettings").GetSection("audience").Value;
                var _issuer = _configuration.GetSection("JwtSettings").GetSection("issuer").Value;

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = new Guid(jwtToken.Claims.First(x => x.Type == "Id").Value);

                // attach user to context on successful jwt validation
                var user = userService.GetById(userId).ConfigureAwait(false).GetAwaiter().GetResult();

                context.Items["User"] = _mapper.Map<UserViewModel>(user); 
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}