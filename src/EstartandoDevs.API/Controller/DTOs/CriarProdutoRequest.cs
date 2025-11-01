namespace EstartandoDevs.API.Controller.DTOs
{
    public class CriarProdutoRequest
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }

        public CriarProdutoRequest(string nome, string descricao, decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;           
        }
    }
}
