using Dapper;
using Guideline.Domain.Entities;
using Guideline.Domain.Interfaces;
using Guideline.Infra.Data.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User> CreateAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(UserQuery.INSERT, new
                {
                    user.Id,
                    user.Name,
                    user.Login,
                    user.Email,
                    user.Document,
                    user.Pass,
                    user.Created
                });
            }

            return user;
        }

        public async Task<User> GetByLoginAsync(string login, string pass)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>($"{UserQuery.SELECT} WHERE [login] = @login AND [pass] = @pass", new { login = login, pass = pass });
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<User>(UserQuery.SELECT);
            }
        }

        public async Task<IEnumerable<User>> GetWithDocumentsAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<User>(UserQuery.SELECTUSERWITHDOCUMENT);
            }
        }

        public async Task<IEnumerable<User>> GetByDocumentAsync(string document)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<User>(UserQuery.SELECTUSERBYDOCUMENT, new { document = document });
            }
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>($"{UserQuery.SELECT} WHERE [Id] = @id", new { id = id.ToString() });
            }
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(UserQuery.DELETE, new { id = id.ToString() });

                return id;
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(UserQuery.UPDATE, new
                {
                    user.Name,
                    user.Login,
                    user.Email,
                    user.Document,
                    user.Id
                });

                return user;
            }
        }
    }
}
