using Amazon.CognitoIdentityProvider.Model;
using EstartandoDevs.API.Controller.Base;
using EstartandoDevs.API.Controller.DTOs;
using EstartandoDevs.Application.CasosDeUso.Autenticacao.CadastrarNovoUsuario;
using EstartandoDevs.Application.CasosDeUso.Autenticacao.ObterToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstartandoDevs.API.Controller
{
    public class AutenticacaoController : BaseController
    {
        private readonly IMediator _mediator;

        public AutenticacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("cadastro")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastro([FromBody] CadastrarNovoUsuarioRequest request, CancellationToken cancellationToken)
        {
            var command = new CadastrarNovoUsuarioCommand(
                 email: request.Email,
                 password: request.Password);

            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Signs in and returns tokens (Cognito integration should be added here)
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterTokenDeAcesso([FromBody] ObterTokenRequest request, CancellationToken cancellationToken)
        {
            var command = new ObterTokenCommand(
                email: request.Email,
                senha: request.Password);

            var response = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)response.StatusCode, response);
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
