using Guideline.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Guideline.Application.Interfaces
{
    public interface ITokenService
    {
        IConfiguration Configuration
        {
            get;
            set;
        }
        Task<TokenResponse> GenerateTokenAsync(string login, string pass);
    }
}
