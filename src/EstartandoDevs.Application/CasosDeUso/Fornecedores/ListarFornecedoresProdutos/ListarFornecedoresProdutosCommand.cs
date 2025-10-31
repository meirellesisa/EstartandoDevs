using EstartandoDevs.Application.Mediator;
using MediatR;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos
{
    public class ListarFornecedoresProdutosCommand : IRequest<CommandResponse<List<ListarFornecedoresProdutosCommandResponse>>>{}
}
