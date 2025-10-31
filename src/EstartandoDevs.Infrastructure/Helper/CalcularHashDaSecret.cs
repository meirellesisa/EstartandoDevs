using System.Security.Cryptography;
using System.Text;

namespace EstartandoDevs.Infrastructure.Helper
{
    public static class CalcularHashDaSecret
    {
        public static string CalculateSecretHash(string username)
        {
            var clientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
            if (string.IsNullOrWhiteSpace(clientId))
                throw new Exception("Client Id do Cognito não configurado.");

            var clientSecret = Environment.GetEnvironmentVariable("COGNITO_CLIENT_SECRET");
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new Exception("Client Secret do Cognito não configurado.");

            var mensagem = username + clientId;
            var bytesDaChave = Encoding.UTF8.GetBytes(clientSecret);
            var bytesDaMensagem = Encoding.UTF8.GetBytes(mensagem);

            using var hmac = new HMACSHA256(bytesDaChave);
            var hashBytes = hmac.ComputeHash(bytesDaMensagem);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
