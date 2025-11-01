using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Globalization;
using System.Linq;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.AtribuirProdutoFornecedor
{
    public class AtribuirProdutoFornecedorCommandHandler : IRequestHandler<AtribuirProdutoFornecedorCommand, CommandResponse<AtribuirProdutoFornecedorCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoRepository _produtoRespository;

        public AtribuirProdutoFornecedorCommandHandler(IFornecedorRepository fornecedorRepository, IProdutoRepository produtoRespository)
        {
            _fornecedorRepository = fornecedorRepository;
            _produtoRespository = produtoRespository;
        }

        public async Task<CommandResponse<AtribuirProdutoFornecedorCommandResponse>> Handle(AtribuirProdutoFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<AtribuirProdutoFornecedorCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var fornecedor = await _fornecedorRepository.ObterPorId(request.FornecedorId);

                if(fornecedor == null)
                {
                    return CommandResponse<AtribuirProdutoFornecedorCommandResponse>.AdicionarErro("Fornecedor não encontrado.", HttpStatusCode.NotFound);
                }

                var produto = new Produto(fornecedor.Id,request.Nome, request.Descricao, request.Valor);

                //fornecedor.AtribuirProduto(produto);
                // analisar se é possivel 
                //await _produtoRespository.Adicionar(produto);

                await _produtoRespository.Adicionar(produto);

                var fornecedorProdutos = await _fornecedorRepository.ObterFornecedorProdutos(fornecedor.Id);

                var produtoResponse = fornecedorProdutos.Produtos.Select(produto => new AtribuirProdutoFornecedor_Produto(
                        id: produto.Id,
                        nome: produto.Nome,
                        descricao: produto.Descricao,
                        valor: Convert.ToDecimal(produto.Valor).ToString("0.00", CultureInfo.InvariantCulture))
                ).ToList();

                var response = new AtribuirProdutoFornecedorCommandResponse(
                    fornecedorId: fornecedorProdutos.Id,
                    nome: fornecedorProdutos.Nome ?? string.Empty,
                    documento: fornecedorProdutos.Documento ?? string.Empty,
                    produtos: produtoResponse );

                return CommandResponse<AtribuirProdutoFornecedorCommandResponse>.Sucesso(response, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CommandResponse<AtribuirProdutoFornecedorCommandResponse>.ErroCritico(ex.Message);
            }
        }
    }
}
