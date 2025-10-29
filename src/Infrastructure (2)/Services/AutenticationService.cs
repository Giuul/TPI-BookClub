using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AutenticacionServiceOptions
    {
        public const string AutenticacionService = "AutenticacionService";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretForKey { get; set; } = string.Empty;
    }

    public class AutenticacionService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;

        public AutenticacionService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string?> AutenticarAsync(AuthenticationRequest authenticationRequest)
        {
            var user = await _userRepository.GetByEmailAsync(authenticationRequest.Email);

            if (user == null)
            {
                return null;
            }

            if (user.Password != authenticationRequest.Password)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Rol.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}