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
        public IConfiguration Configuration  // read-write instance property
        {
            get => _configuration;
            set => _configuration = value;
        }

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TokenService(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateTokenAsync(string login, string pass)
        {
            var user = await _userRepository.Get(login, pass);           

            if (user != null)
            {
                var userViewModel = _mapper.Map<UserViewModel>(user);

                var _secret = Configuration.GetSection("JwtSettings").GetSection("secret").Value;
                var _expDate = DateTime.UtcNow.AddMinutes(double.Parse(Configuration.GetSection("JwtSettings").GetSection("expiration").Value));
                var _audience = Configuration.GetSection("JwtSettings").GetSection("audience").Value;
                var _issuer = Configuration.GetSection("JwtSettings").GetSection("issuer").Value;

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
                return tokenHandler.WriteToken(token);
            }

            return string.Empty;
        }

    }
}
