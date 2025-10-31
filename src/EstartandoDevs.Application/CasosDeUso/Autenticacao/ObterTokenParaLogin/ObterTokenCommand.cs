using EstartandoDevs.Application.Mediator;
using MediatR;

namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.ObterToken
{
    public class ObterTokenCommand : IRequest<CommandResponse<ObterTokenCommandResponse>>
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public ObterTokenCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        //VALIDAÇÃO 
    }
}
