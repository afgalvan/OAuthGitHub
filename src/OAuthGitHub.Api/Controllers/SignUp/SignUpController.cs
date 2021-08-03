using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OAuthGitHub.Api.Controllers.Shared;
using OAuthGithub.Core.Application;

namespace OAuthGitHub.Api.Controllers.SignUp
{
    [Route("auth")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IMediator                 _mediator;
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(ILogger<SignUpController> logger, IMediator mediator)
        {
            _logger   = logger;
            _mediator = mediator;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<AuthenticationResponse>> SignUp(
            [FromBody] SignUpRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(request.Username, request.Email, request.Password);
            try
            {
                string token = await _mediator.Send(command, cancellationToken);
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
