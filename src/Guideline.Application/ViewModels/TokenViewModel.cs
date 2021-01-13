namespace Guideline.Application.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }

        public TokenViewModel(string token = null)
        {
            Token = token;
        }
    }
}
