﻿using Guideline.Application.ViewModels;
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
        Task<TokenViewModel> GenerateTokenAsync(string login, string pass);
    }
}
