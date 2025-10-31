using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using EstartandoDevs.Application.Mediator;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;



namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.RealizarLogout
{
    public class RealizarLogoutCommandHandler : IRequestHandler<RealizarLogoutCommand, CommandResponse<Unit>>
    {
        public readonly IAmazonCognitoIdentityProvider _cognito;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RealizarLogoutCommandHandler(IAmazonCognitoIdentityProvider cognito, IHttpContextAccessor httpContextAccessor)
        {
            _cognito = cognito;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CommandResponse<Unit>> Handle(RealizarLogoutCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return CommandResponse<Unit>.AdicionarErro("Contexto HTTP não encontrado", HttpStatusCode.InternalServerError);

            var bearer = httpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(bearer) || !bearer.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return CommandResponse<Unit>.AdicionarErro("Usuario sem acesso", HttpStatusCode.Unauthorized);
            }
            var accessToken = bearer.Substring("Bearer ".Length).Trim();

            await _cognito.GlobalSignOutAsync(new GlobalSignOutRequest
            {
                AccessToken = accessToken
            }, cancellationToken);

            return CommandResponse<Unit>.Sucesso("Logout realizado com sucesso", HttpStatusCode.NoContent);
        }
    }
}
