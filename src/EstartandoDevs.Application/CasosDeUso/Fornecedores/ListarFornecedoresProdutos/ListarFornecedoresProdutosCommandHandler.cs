using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos
{
    public class ListarFornecedoresProdutosCommandHandler : IRequestHandler<ListarFornecedoresProdutosCommand, CommandResponse<ListarFornecedoresProdutosCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public ListarFornecedoresProdutosCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        public Task<CommandResponse<ListarFornecedoresProdutosCommandResponse>> Handle(ListarFornecedoresProdutosCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
