using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos
{
    public class ListarFornecedoresProdutosCommandHandler : IRequestHandler<ListarFornecedoresProdutosCommand, CommandResponse<List<ListarFornecedoresProdutosCommandResponse>>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public ListarFornecedoresProdutosCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        public async Task<CommandResponse<List<ListarFornecedoresProdutosCommandResponse>>> Handle(ListarFornecedoresProdutosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fornecedoresComProdutos = await _fornecedorRepository.ObterTodos();

                var response = fornecedoresComProdutos.Select(fornecedor => new ListarFornecedoresProdutosCommandResponse(
                    fornecedor.Id,
                    fornecedor.Nome,
                    fornecedor.Documento,
                    (int)fornecedor.TipoFornecedor
                )).ToList();

                return CommandResponse<List<ListarFornecedoresProdutosCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommandResponse<List<ListarFornecedoresProdutosCommandResponse>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os fornecedores: {ex.Message}");
            }
        }
    }
}
