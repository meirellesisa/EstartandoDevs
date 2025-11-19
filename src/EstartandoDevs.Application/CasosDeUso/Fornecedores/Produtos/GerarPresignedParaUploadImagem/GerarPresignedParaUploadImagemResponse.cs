namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.GerarPresignedParaUploadImagem
{
    public class GerarPresignedParaUploadImagemResponse
    {
        public string PresignedUrl { get; private set; }

        public GerarPresignedParaUploadImagemResponse(string presignedUrl)
        {
            PresignedUrl = presignedUrl;
        }
    }
}
