namespace EstartandoDevs.API.Controller.DTOs
{
    public class CadastrarNovoUsuarioRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ObterTokenRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
