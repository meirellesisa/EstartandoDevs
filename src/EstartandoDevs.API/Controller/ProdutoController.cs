using EstartandoDevs.API.Controller.Base;
using EstartandoDevs.API.Controller.DTOs;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.AtribuirProdutoFornecedor;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.GerarPresignedParaUploadImagem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstartandoDevs.API.Controller
{

    [Route("api/")]
    public class ProdutoController : BaseController
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("fornecedores/{fornecedorId}/produtos")]
        public async Task<IActionResult> CriarProduto(
          [FromBody] CriarProdutoRequest request,
          Guid fornecedorId)
        {
            var command = new AtribuirProdutoFornecedorCommand(
                fornecedorId: fornecedorId,
                nome: request.Nome,
                descricao: request.Descricao,
                valor: request.Valor);

            var response = await _mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        [Route("fornecedores/{fornecedorId}/produtos/{produtoId}/upload")]
        public async Task<IActionResult> GeerarPresignedUrlParaUpload(
          [FromBody] GerarPresignedUrlParaUploadRequest request,
          [FromRoute] Guid fornecedorId,
          [FromRoute] Guid produtoId)
        {
            var command = new GerarPresignedParaUploadImagemCommand(
                fornecedorId: fornecedorId,
                produtoId: produtoId,
                nomeArquivo: request.NomeArquivo);

            var response = await _mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
