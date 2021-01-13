using Guideline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<User> Get(string login, string pass);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetWithDocuments();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<Guid> Remove(Guid id);
    }
}
