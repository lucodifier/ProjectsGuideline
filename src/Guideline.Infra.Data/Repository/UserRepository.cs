using Dapper;
using Guideline.Domain.Configuration;
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

        public async Task<User> Add(User user)
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

        public async Task<User> Get(string login, string pass)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>($"{UserQuery.SELECT} WHERE [login] = @login AND [pass] = @pass", new { login = login, pass = pass });
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<User>(UserQuery.SELECT);
            }
        }

        public async Task<IEnumerable<User>> GetWithDocuments()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<User>(UserQuery.SELECTUSERWITHDOCUMENT);
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>($"{UserQuery.SELECT} WHERE [Id] = @id", new { id = id.ToString() });
            }
        }

        public async Task<Guid> Remove(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(UserQuery.DELETE, new { id = id.ToString() });

                return id;
            }
        }

        public async Task<User> Update(User user)
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
