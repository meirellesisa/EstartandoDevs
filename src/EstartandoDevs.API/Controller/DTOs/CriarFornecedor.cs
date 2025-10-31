namespace EstartandoDevs.API.Controller.DTOs
{
    public record CriarFornecedor(string Nome,string Documento, int TipoFornecedor){}
    public record EditarFornecedor(string Nome){}
}