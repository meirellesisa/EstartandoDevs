using EstartandoDevs.Application.Mediator;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorEndereco
{
    public class ObterFornecedorEnderecoCommand : IRequest<CommandResponse<ObterFornecedorEnderecoCommandResponse>>
    {
        public Guid FornecedorId { get; private set; }
        public ValidationResult ResultadoDasValidacoes { get; private set; }

        public ObterFornecedorEnderecoCommand(Guid fornecedorId)
        {
            FornecedorId = fornecedorId;
        }

        public bool Validar()
        {
            var validacoes = new InlineValidator<ObterFornecedorEnderecoCommand>();

            validacoes.RuleFor(fornecedor => fornecedor.FornecedorId)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O ID do fornecedor deve ser informado.");

            ResultadoDasValidacoes = validacoes.Validate(this);

            return ResultadoDasValidacoes.IsValid;
        }

    }
}
