namespace EstartandoDevs.Application.CasosDeUso.Autenticacao.ObterToken
{
    public class ObterTokenCommandResponse
    {
        public string AccessToken { get; private set; } = string.Empty;
        public string IdToken { get; private set; } = string.Empty;
        public string RefreshToken { get; private set; } = string.Empty;
        public string ExpiresIn { get; private set; }
        public string TokenType { get; private set;}

        public ObterTokenCommandResponse(
            string acessToken,
            string idToken,
            string refreshToken,
            string expiresIn,
            string tokenType)
        {
            AccessToken = acessToken;
            IdToken = idToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            TokenType = tokenType;     
        }
    }
}
