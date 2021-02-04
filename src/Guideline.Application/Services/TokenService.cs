using AutoMapper;
using Guideline.Application.Interfaces;
using Guideline.Application.ViewModels;
using Guideline.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Guideline.Application.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration;

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TokenService(
            IConfiguration configuration,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<TokenResponse> GenerateTokenAsync(string login, string pass)
        {
            var user = await _userRepository.GetByLoginAsync(login, pass);           

            if (user != null)
            {
                var userViewModel = _mapper.Map<UserResponse>(user);

                var _secret = _configuration.GetSection("JwtSettings").GetSection("secret").Value;
                var _expDate = DateTime.UtcNow.AddMinutes(double.Parse(_configuration.GetSection("JwtSettings").GetSection("expiration").Value));
                var _audience = _configuration.GetSection("JwtSettings").GetSection("audience").Value;
                var _issuer = _configuration.GetSection("JwtSettings").GetSection("issuer").Value;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", userViewModel.Id.ToString()),
                        new Claim("Login", userViewModel.Login),
                        new Claim("Email", userViewModel.Email),
                    
                   }),
                    Issuer = _issuer,
                    Audience = _audience,
                    Expires = _expDate,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new TokenResponse(tokenHandler.WriteToken(token));
            }

            return new TokenResponse();
        }

    }
}
