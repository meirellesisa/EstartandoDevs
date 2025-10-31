using EstartandoDevs.Application.Mediator;
using MediatR;

namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.RealizarLogout
{
    public class RealizarLogoutCommand : IRequest<CommandResponse<Unit>>
    {
    }
}
