using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorProdutosEndereco
{
    public class ObterFornecedorProdutosEnderecoCommandHandler : IRequestHandler<ObterFornecedorProdutosEnderecoCommand, CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public ObterFornecedorProdutosEnderecoCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>> Handle(ObterFornecedorProdutosEnderecoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var fornecedorProdutosEndereco = await _fornecedorRepository.ObterFornecedorProdutosEndereco(request.FornecedorId);

                if (fornecedorProdutosEndereco == null)
                {
                    return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.AdicionarErro("Fornecedor não encontrado.", System.Net.HttpStatusCode.NotFound);
                }

                var response = new ObterFornecedorProdutosEnderecoCommandResponse(
                    fornecedorProdutosEndereco.Id,
                    fornecedorProdutosEndereco.Nome ?? string.Empty,
                    fornecedorProdutosEndereco.Documento ?? string.Empty,
                    fornecedorProdutosEndereco.TipoFornecedor,
                    endereco: new ObterFornecedorProdutosEndereco_Endereco(
                        fornecedorProdutosEndereco.Endereco.Id,
                        fornecedorProdutosEndereco.Endereco.Logradouro ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Numero ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Complemento ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Bairro ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Cep ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Cidade ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Estado ?? string.Empty),
                    produtos: fornecedorProdutosEndereco.Produtos.Select(fpe =>
                         new ObterFornecedorProdutosEndereco_Produtos(
                              fpe.Nome ?? string.Empty,
                              fpe.Descricao ?? string.Empty,
                              fpe.Valor)).ToList());

                return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.ErroCritico(ex.Message);
            }
        }
    }
}
