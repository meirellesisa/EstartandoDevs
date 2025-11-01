using EstartandoDevs.Application.Mediator;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.AtribuirProdutoFornecedor
{
    public class AtribuirProdutoFornecedorCommand : IRequest<CommandResponse<AtribuirProdutoFornecedorCommandResponse>>
    {
        public Guid FornecedorId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public ValidationResult ResultadoDasValidacoes { get; private set; }

        public AtribuirProdutoFornecedorCommand(Guid fornecedorId, string nome, string descricao, decimal valor)
        {
            FornecedorId = fornecedorId;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public bool Validar()
        {
            var validacoes = new InlineValidator<AtribuirProdutoFornecedorCommand>();

            validacoes.RuleFor(produtoFornecedor => produtoFornecedor.FornecedorId)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O ID do fornecedor deve ser informado.");

            validacoes.RuleFor(pd => pd.Nome)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O nome do produto deve ser informado.");

            validacoes.RuleFor(pd => pd.Descricao)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("A descrição do produto deve ser informada.");

            validacoes.RuleFor(pd => pd.Valor)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O valor do produto deve ser informado.");

            ResultadoDasValidacoes = validacoes.Validate(this);

            return ResultadoDasValidacoes.IsValid;
        }
    }
}
