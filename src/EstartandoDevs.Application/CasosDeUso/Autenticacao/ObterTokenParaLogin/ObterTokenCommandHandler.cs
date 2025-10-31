using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Infrastructure.Helper;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.ObterToken
{
    public class ObterTokenCommandHandler : IRequestHandler<ObterTokenCommand, CommandResponse<ObterTokenCommandResponse>>
    {
        public readonly IAmazonCognitoIdentityProvider _cognito;

        public ObterTokenCommandHandler(IAmazonCognitoIdentityProvider cognito)
        {
            _cognito = cognito;
        }
        public async Task<CommandResponse<ObterTokenCommandResponse>> Handle(ObterTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var parametrosDeAutenticacao = new Dictionary<string, string>
                {
                      { "USERNAME", request.Email },
                      { "PASSWORD", request.Senha }
                };

                parametrosDeAutenticacao["SECRET_HASH"] = CalcularHashDaSecret.CalculateSecretHash(request.Email);

                var authRequest = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    ClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID"),
                    AuthParameters = parametrosDeAutenticacao,
                };

                var authResp = await _cognito.InitiateAuthAsync(authRequest, cancellationToken);

                var response = new ObterTokenCommandResponse(
                    authResp.AuthenticationResult.AccessToken,
                    authResp.AuthenticationResult.IdToken,
                    authResp.AuthenticationResult.RefreshToken,
                    authResp.AuthenticationResult.ExpiresIn.ToString(),
                    authResp.AuthenticationResult.TokenType
                );

                return CommandResponse<ObterTokenCommandResponse>.Sucesso(response, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CommandResponse<ObterTokenCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao obter o token de acesso: {ex.Message}");
            }
        }
    }

}
