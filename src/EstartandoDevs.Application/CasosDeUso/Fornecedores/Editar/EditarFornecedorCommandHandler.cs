using EstartandoDevs.Application.Mediator;
using EstartandoDevs.Domain.Repository;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Editar
{
    public class EditarFornecedorCommandHandler : IRequestHandler<EditarFornecedorCommand, CommandResponse<Unit>>
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public EditarFornecedorCommandHandler(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<CommandResponse<Unit>> Handle(EditarFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            var fornecedor = await _fornecedorRepository.ObterPorId(request.Id);

            if (fornecedor == null)
            {
                return CommandResponse<Unit>.AdicionarErro("Fornecedor não encontrado.", HttpStatusCode.NotFound);
            }

            var nomeJaUtilizado = await _fornecedorRepository.NomeJaUtilizado(request.Nome);

            if (nomeJaUtilizado)
            {
                return CommandResponse<Unit>.AdicionarErro("O nome do fornecedor já está em uso.", HttpStatusCode.Conflict);
            }

            // documento precisa ser validado

            fornecedor.AtribuirNome(request.Nome);
            fornecedor.AtribuirDocumento(request.Documento);
            fornecedor.AtribuirTipoFornecedor(request.TipoFornecedor);

            await _fornecedorRepository.Atualizar(fornecedor);

            return CommandResponse<Unit>.Sucesso(null,HttpStatusCode.OK);
        }    
    }
}
