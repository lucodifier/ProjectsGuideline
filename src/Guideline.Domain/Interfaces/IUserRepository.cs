using Guideline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByLoginAsync(string login, string pass);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetWithDocumentsAsync();
        Task<IEnumerable<User>> GetByDocumentAsync(string document);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<Guid> RemoveAsync(Guid id);
    }
}
