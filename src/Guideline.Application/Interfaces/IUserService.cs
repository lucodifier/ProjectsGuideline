using FluentValidation.Results;
using Guideline.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> Get(string login, string pass);
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<IEnumerable<UserViewModel>> GetWithDocument();
        Task<IEnumerable<UserViewModel>> GetByDocument(string document);
        Task<UserViewModel> GetById(Guid id);
        Task<ValidationResultViewModel> Create(CreateUserViewModel createUserViewModel);
        Task<ValidationResultViewModel> Update(UpdateUserViewModel updateUserViewModel);
        Task<Guid> Remove(Guid id);
    }
}
