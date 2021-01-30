using Guideline.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> Get(string login, string pass);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<IEnumerable<UserResponse>> GetWithDocumentAsync();
        Task<IEnumerable<UserResponse>> GetByDocumentAsync(string document);
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<CreatedUserResponse> CreateAsync(CreateUserRequest createUserViewModel);
        Task<CreatedUserResponse> UpdateAsync(UpdateUserRequest updateUserViewModel);
        Task<Guid> RemoveAsync(Guid id);
    }
}
