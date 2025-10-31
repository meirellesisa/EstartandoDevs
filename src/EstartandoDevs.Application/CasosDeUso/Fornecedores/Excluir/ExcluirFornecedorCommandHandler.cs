using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Excluir
{
    public class ExcluirFornecedorCommandHandler : IRequestHandler<ExcluirFornecedorCommand, CommandResponse<Unit>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public ExcluirFornecedorCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<CommandResponse<Unit>> Handle(ExcluirFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            //obter o fornecedor do banco de dado -> verificar se existe -> excluir
            // retorn 404
            var fornecedor = await _fornecedorRepository.ObterPorId(request.Id);

            if (fornecedor == null)
            {
                return CommandResponse<Unit>.AdicionarErro("Fornecedor não encontrado.", HttpStatusCode.NotFound);
            }

            await _fornecedorRepository.Remover(fornecedor.Id); 

            return CommandResponse<Unit>.Sucesso(null, HttpStatusCode.NoContent);
        }
    }
}
