using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Cloud.UploadArquivoS3.Interface;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.GerarPresignedParaUploadImagem
{
    public class GerarPresignedParaUploadImagemCommandHandler : IRequestHandler<GerarPresignedParaUploadImagemCommand, CommandResponse<GerarPresignedParaUploadImagemResponse>>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IAwsS3Service _awsS3Service;

        public GerarPresignedParaUploadImagemCommandHandler(
            IProdutoRepository produtoRepository, 
            IAwsS3Service awsS3Service)
        {
            _produtoRepository = produtoRepository;
            _awsS3Service = awsS3Service;
        }
        public async Task<CommandResponse<GerarPresignedParaUploadImagemResponse>> Handle(GerarPresignedParaUploadImagemCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<GerarPresignedParaUploadImagemResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            // verificar se o produto existe e se ele pertence ao fornecedor que foi passado 
            var produto = await _produtoRepository.ProdutoExisteEPertenseAoFornecedor(request.FornecedorId, request.ProdutoId);

            // validar se a respostar anterior é igual a nulo, caso seja retorna uma resposta de acordo pro usuário
            if(produto == null)
            {
                // retornar uma resposta de acordo pro usuário
                return CommandResponse<GerarPresignedParaUploadImagemResponse>
                    .AdicionarErro("Produto não encontrado ou não pertence ao fornecedor.", HttpStatusCode.NotFound);
            }

            // validar se o arquivo já existe no banco 

            // validar o nome do arquivo (extensão, tamanho, etc)
            var nomeArquivo = request.NomeArquivo.Trim();

            // construir o caminho desse arquivo no bucket s3
            var caminhoArquivoNoBucket = $"fornecedores/{request.FornecedorId}/produtos/{request.ProdutoId}";

            // gerar a presigned ur para upload no s3
            var presignedUrl = await GerarPresignedUrlParaUpload(caminhoArquivoNoBucket, request.NomeArquivo);

            produto.AtribuirNomeArquivo(nomeArquivo);
            await _produtoRepository.Atualizar(produto);

            // montar o nosso response 
            var response = new GerarPresignedParaUploadImagemResponse(presignedUrl);

            return CommandResponse<GerarPresignedParaUploadImagemResponse>.Sucesso(response, HttpStatusCode.OK);
        }

        private async Task<string> GerarPresignedUrlParaUpload(string caminhoArquivoNoBucket, string nomeArquivo)
        {
            var presignedUrl = await _awsS3Service.GerarPresignedUrlUploadAsync(caminhoArquivoNoBucket, nomeArquivo);
            return presignedUrl;
        }
    }
}
