using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedor.Criar
{
    public class CriarFornecedorCommandHandler : IRequestHandler<CriarFornecedorCommand, CriarFornecedorCommandResponse>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public CriarFornecedorCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;

        }
        public async Task<CriarFornecedorCommandResponse> IRequestHandler<CriarFornecedorCommand, CriarFornecedorCommandResponse>.Handle(CriarFornecedorCommand request, CancellationToken cancellationToken)
        {
            // Validar nosso command 
            if (!request.Validar())
            {
                var erros = request.ResultadoDasValidacoes.Errors
                    .Select(f => new {mensagem = f.ErrorMessage, statusCode = f.ErrorCode})
                    .ToList();
                return CriarFornecedorCommandResponse.ErroValidacao(mensagem: erros.Select(f => f.mensagem) );
            }


            //Salvar o fornecedor no banco de dados


            // Retornar uma resposta pro usuário 
        }
    }
}
