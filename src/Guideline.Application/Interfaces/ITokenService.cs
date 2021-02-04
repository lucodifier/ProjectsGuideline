using Guideline.Application.ViewModels;
using System.Threading.Tasks;

namespace Guideline.Application.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateTokenAsync(string login, string pass);
    }
}
