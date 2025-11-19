namespace EstartandoDevs.Infrastructure.Cloud.UploadArquivoS3.Interface
{
    public interface IAwsS3Service
    {
        Task<string> GerarPresignedUrlUploadAsync(string caminho, string nomeArquivo);
        Task<string> GerarPresignedUrlDownloadAsync( string caminho, string nomeArquivo);
    }
}

