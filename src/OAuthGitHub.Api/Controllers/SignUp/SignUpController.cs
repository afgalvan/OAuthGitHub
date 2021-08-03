using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAuthGitHub.Api.Application;
using OAuthGitHub.Api.Domain;

namespace OAuthGitHub.Api.Infrastructure.Controllers.SignUp
{
    [Route("auth")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly AuthService               _authService;
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(AuthService authService, ILogger<SignUpController> logger)
        {
            _authService = authService;
            _logger      = logger;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<AuthenticationResponse>> SignUp(
            [FromBody] SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = new User(request.Username, request.Email, request.Password);
            try
            {
                string token = await _authService.SignUp(user, cancellationToken);
                _logger.LogInformation("New token retrieved");
                return Ok(new AuthenticationResponse(token));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.StackTrace);
                return BadRequest();
            }
        }
    }
}
