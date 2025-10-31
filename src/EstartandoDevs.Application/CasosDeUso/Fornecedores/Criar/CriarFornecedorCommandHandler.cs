using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar
{
    public class CriarFornecedorCommandHandler : IRequestHandler<CriarFornecedorCommand, CommandResponse<CriarFornecedorCommandResponse>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public CriarFornecedorCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;

        }
        public async Task<CommandResponse<CriarFornecedorCommandResponse>> Handle(CriarFornecedorCommand request, CancellationToken cancellationToken)
        {
            // Validar nosso command 
            if (!request.Validar())
            {        
                return CommandResponse<CriarFornecedorCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var nomeJaUtilizado = await _fornecedorRepository.NomeJaUtilizado(request.Nome);

                if (nomeJaUtilizado)
                {
                    return CommandResponse<CriarFornecedorCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do fornecedor já está em uso.");
                }

                // Criar nossa entidade Fornecedor
                var fornecedor = new Fornecedor(request.Nome, request.Documento, request.TipoFornecedor);

                //Salvar o fornecedor no banco de dados
                await _fornecedorRepository.Adicionar(fornecedor);

                var response = new CriarFornecedorCommandResponse(fornecedor.Id);

                // Retornar uma resposta pro usuário 
                return CommandResponse<CriarFornecedorCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return CommandResponse<CriarFornecedorCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o fornecedor: {ex.Message}");
            }

        }
    }
}
