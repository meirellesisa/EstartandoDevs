using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Cloud.UploadArquivoS3.Interface;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorProdutosEndereco
{
    public class ObterFornecedorProdutosEnderecoCommandHandler : IRequestHandler<ObterFornecedorProdutosEnderecoCommand, CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IAwsS3Service _awsS3Service;

        public ObterFornecedorProdutosEnderecoCommandHandler(
            IFornecedorRepository fornecedorRepository, 
            IAwsS3Service awsS3Service)
        {
            _fornecedorRepository = fornecedorRepository;
            _awsS3Service = awsS3Service;
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
                    return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.AdicionarErro("Fornecedor não encontrado.", HttpStatusCode.NotFound);
                }

                var enderecoResponse = fornecedorProdutosEndereco.Endereco == null
                    ? null
                    : new ObterFornecedorProdutosEndereco_Endereco(
                        fornecedorProdutosEndereco.Endereco.Id,
                        fornecedorProdutosEndereco.Endereco.Logradouro ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Numero ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Complemento ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Bairro ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Cep ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Cidade ?? string.Empty,
                        fornecedorProdutosEndereco.Endereco.Estado ?? string.Empty);

                var produtosResponse = fornecedorProdutosEndereco.Produtos?.Select(async produto =>
                {
                     var preview = await _awsS3Service.GerarPresignedUrlDownloadAsync(
                                   caminho: $"fornecedores/{produto.FornecedorId}/produtos/{produto.Id}",
                                   nomeArquivo: produto.NomeArquivo ?? string.Empty);

                    return new ObterFornecedorProdutosEndereco_Produtos(
                        produto.Nome ?? string.Empty,
                        produto.Descricao ?? string.Empty,
                        preview,
                        produto.Valor);
                });

                var responseProdutos = await Task.WhenAll(produtosResponse);
            
                var teste = fornecedorProdutosEndereco.Produtos?.Select(produto =>
                {
                    var dado = _awsS3Service.GerarPresignedUrlDownloadAsync(
                        $"fornecedores/{produto.FornecedorId}/produtos/{produto.Id}",
                        produto.NomeArquivo ?? string.Empty);

                    
                    return dado;
                });

                var response = new ObterFornecedorProdutosEnderecoCommandResponse(
                    fornecedorProdutosEndereco.Id,
                    fornecedorProdutosEndereco.Nome ?? string.Empty,
                    fornecedorProdutosEndereco.Documento ?? string.Empty,
                    fornecedorProdutosEndereco.TipoFornecedor,
                    endereco: enderecoResponse,
                    produtos: responseProdutos.ToList());

                return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommandResponse<ObterFornecedorProdutosEnderecoCommandResponse>.ErroCritico(ex.Message);
            }
        }
    }
}
