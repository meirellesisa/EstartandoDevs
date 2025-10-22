using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar
{
    public class CriarFornecedorCommandHandler : IRequestHandler<CriarFornecedorCommand, CriarFornecedorCommandResponse>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public CriarFornecedorCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;

        }
        public async Task<CriarFornecedorCommandResponse> Handle(CriarFornecedorCommand request, CancellationToken cancellationToken)
        {
            // Validar nosso command 
            if (!request.Validar())
            {
                var erros = request.ResultadoDasValidacoes.Errors
                    .Select(f => new { mensagem = f.ErrorMessage, statusCode = f.ErrorCode })
                    .ToList();

                return CriarFornecedorCommandResponse.ErroValidacao(mensagem: erros.First().mensagem, statusCode: erros.First().statusCode);
            }

            try
            {
                // Criar nossa entidade Fornecedor
                var fornecedor = new Fornecedor(request.Nome, request.Documento, request.TipoFornecedor);

                //Salvar o fornecedor no banco de dados
                await _fornecedorRepository.Adicionar(fornecedor);

                // Retornar uma resposta pro usuário 
                var response = CriarFornecedorCommandResponse.Sucesso(dados: fornecedor.Id, statusCode: HttpStatusCode.Created);

                return response;

            }catch(Exception ex)
            {
                return CriarFornecedorCommandResponse.ErroInterno(mensagem: $"Ocorreu um erro ao criar o fornecedor: {ex.Message}");
            }
          
        }
    }
}
