using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        public SignUpController(IMediator mediator, ILogger<SignUpController> logger)
        {
            _mediator = mediator;
            _logger   = logger;
        }

        [HttpPost("signUp")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> SignUp(
            [FromBody] SignUpRequest request, CancellationToken cancellationToken)
        {
            var command =
                new RegisterCommand(request.Username, request.Email, request.Password);
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
