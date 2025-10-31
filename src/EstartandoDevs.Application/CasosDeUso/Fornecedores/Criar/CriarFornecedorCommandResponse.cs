namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar
{
    public class CriarFornecedorCommandResponse
    {
        public Guid Id { get; private set; }

        public CriarFornecedorCommandResponse(Guid id)
        {
            Id = id;
        }
    }
}
