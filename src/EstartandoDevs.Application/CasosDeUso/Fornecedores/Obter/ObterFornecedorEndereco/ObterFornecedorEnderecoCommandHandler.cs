using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorEndereco
{
    public class ObterFornecedorEnderecoCommandHandler : IRequestHandler<ObterFornecedorEnderecoCommand, CommandResponse<ObterFornecedorEnderecoCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public ObterFornecedorEnderecoCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        public async Task<CommandResponse<ObterFornecedorEnderecoCommandResponse>> Handle(ObterFornecedorEnderecoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<ObterFornecedorEnderecoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var fornecedor = await _fornecedorRepository.ObterFornecedorEndereco(request.FornecedorId);

                if (fornecedor == null)
                {
                    return CommandResponse<ObterFornecedorEnderecoCommandResponse>.AdicionarErro("Fornecedor não encontrado.", HttpStatusCode.NotFound);
                }

                var fornecedorEndereco = new ObterFornecedorEnderecoCommandResponse(
                    id: fornecedor.Id,
                    nome: fornecedor.Nome ?? string.Empty,
                    documento: fornecedor.Documento ?? string.Empty,
                    tipoFornecedor: fornecedor.TipoFornecedor,
                    endereco: new ObterFornecedorEndereco_Endereco(
                        fornecedor.Endereco.Id,
                        fornecedor.Endereco.Logradouro ?? string.Empty,
                        fornecedor.Endereco.Numero ?? string.Empty,
                        fornecedor.Endereco.Complemento ?? string.Empty,
                        fornecedor.Endereco.Bairro ?? string.Empty,
                        fornecedor.Endereco.Cep ?? string.Empty,
                        fornecedor.Endereco.Cidade ?? string.Empty,
                        fornecedor.Endereco.Estado ?? string.Empty));

                return CommandResponse<ObterFornecedorEnderecoCommandResponse>.Sucesso(fornecedorEndereco, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CommandResponse<ObterFornecedorEnderecoCommandResponse>.ErroCritico(ex.Message);
            }
        }
    }
}
