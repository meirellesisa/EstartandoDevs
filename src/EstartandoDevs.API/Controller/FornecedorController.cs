using EstartandoDevs.API.Controller.Base;
using EstartandoDevs.API.Controller.DTOs;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Editar;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Excluir;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorProdutosEndereco;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstartandoDevs.API.Controller
{
    [Authorize]
    public class FornecedorController : BaseController
    {
        private readonly IMediator _mediator;

        public FornecedorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("fornecedores")]
        public async Task<IActionResult> ObterTodos()
        {
            var command = new ListarFornecedoresProdutosCommand();
            var response = await _mediator.Send(command, CancellationToken.None);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Route("fornecedores/{idFornecedor}")]
        public async Task<ActionResult> ObterFornecedorProdutosEnderecoPorId(Guid idFornecedor)
        {
            var command = new ObterFornecedorProdutosEnderecoCommand(idFornecedor);

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        [Route("fornecedores")]
        public async Task<IActionResult> CriarFornecedor(
            [FromBody] CriarFornecedor request)
        {
            var command = new CriarFornecedorCommand(
                nome: request.Nome,
                documento: request.Documento,
                tipoFornecedor: request.TipoFornecedor);

            var response = await _mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        [Route("fornecedores/{idFornecedor}")]
        public async Task<ActionResult> Atualizar(
            Guid idFornecedor,
            [FromBody] EditarFornecedor request)
        {
            var command = new EditarFornecedorCommand(
                id: idFornecedor,
                nome: request.Nome);

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete]
        [Route("fornecedores/{idFornecedor}")]
        public async Task<ActionResult> Excluir(Guid idFornecedor)
        {
            var command = new ExcluirFornecedorCommand(
                 id: idFornecedor);

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);
        }

    }
}
