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
        Task<string> GenerateTokenAsync(string login, string pass);
    }
}
