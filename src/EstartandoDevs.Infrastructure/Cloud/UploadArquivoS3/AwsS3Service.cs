using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using EstartandoDevs.Infrastructure.Cloud.UploadArquivoS3.Interface;

namespace EstartandoDevs.Infrastructure.Cloud.UploadArquivoS3
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _s3Client;

        public AwsS3Service()
        {
            var accessKey = Environment.GetEnvironmentVariable("AWS_S3_ACCESS_KEY");
            var secretKey = Environment.GetEnvironmentVariable("AWS_S3_SECRET_KEY");
            var region = Environment.GetEnvironmentVariable("AWS_S3_REGION");

            _s3Client = new AmazonS3Client(accessKey,secretKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task<string> GerarPresignedUrlUploadAsync(string caminho, string nomeArquivo)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME"),
                Key = $"{caminho}/{nomeArquivo}",
                Expires = DateTime.UtcNow.AddMinutes(15),
                Verb = HttpVerb.PUT
            };

            var response = await _s3Client.GetPreSignedURLAsync(request);

            return response;
        }
        public async Task<string> GerarPresignedUrlDownloadAsync( string caminho, string nomeArquivo)
        {
            if(string.IsNullOrEmpty(nomeArquivo))
                return string.Empty;
            var request = new GetPreSignedUrlRequest
            {
                BucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME"),
                Key = $"{caminho}/{nomeArquivo}",
                Expires = DateTime.UtcNow.AddMinutes(15),
                Verb = HttpVerb.GET
            };

            var response = await _s3Client.GetPreSignedURLAsync(request);

            return response;
        }
    }
}
