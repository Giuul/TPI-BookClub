using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TPI_BookClub.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthController(ICustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ??
                throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequest authenticationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authenticationService.AutenticarAsync(authenticationRequest);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas. Email o contraseña incorrectos." });
            }

            return Ok(new { token = token });
        }
    }
}