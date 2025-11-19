namespace EstartandoDevs.API.Controller.DTOs
{
    public class GerarPresignedUrlParaUploadRequest
    {
        public string NomeArquivo { get; private set; }

        public GerarPresignedUrlParaUploadRequest(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;           
        }
    }
}
