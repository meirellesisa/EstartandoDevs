using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Infrastructure.Helper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.CadastrarNovoUsuario
{
    public class CadastrarNovoUsuarioCommandHandler : IRequestHandler<CadastrarNovoUsuarioCommand, CommandResponse<Unit>>
    {
        private readonly IMediator _mediator;
        private readonly IAmazonCognitoIdentityProvider _cognito;

        public CadastrarNovoUsuarioCommandHandler(IMediator mediator, IConfiguration configuracoes, IAmazonCognitoIdentityProvider cognito)
        {
            _mediator = mediator;
            _cognito = cognito;
        }
        public async Task<CommandResponse<Unit>> Handle(CadastrarNovoUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var signUp = new SignUpRequest
                {
                    ClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID"),
                    Username = request.Email,
                    Password = request.Password,
                    UserAttributes =
                    [
                        new AttributeType { Name = "email", Value = request.Email }
                    ]
                };

                // Add SECRET_HASH if Client Secret is configured
                signUp.SecretHash = CalcularHashDaSecret.CalculateSecretHash(username: request.Email);

                var resp = await _cognito.SignUpAsync(signUp, cancellationToken);

                return CommandResponse<Unit>.Sucesso($"Registro criado com sucesso", statusCode: HttpStatusCode.OK);
            }
            catch (InvalidPasswordException ex)
            {
                return CommandResponse<Unit>.AdicionarErro($"{ex.Message}", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CommandResponse<Unit>.ErroCritico(ex.Message);
            }
        }
    }
}
