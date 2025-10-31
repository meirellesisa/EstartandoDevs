using EstartandoDevs.Application.Mediator;
using MediatR;

namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.CadastrarNovoUsuario
{
    public class CadastrarNovoUsuarioCommand : IRequest<CommandResponse<Unit>>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public CadastrarNovoUsuarioCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
