namespace Guideline.Application.ViewModels
{
    public class TokenResponse : IResponse
    {
        public string Token { get; set; }

        public TokenResponse(string token = null)
        {
            Token = token;
        }
    }
}
