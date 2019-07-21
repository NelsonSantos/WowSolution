using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Entity;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class AccountRepository : RepositoryBase
    {
        public AccountRepository(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using (var _connection = this.GetConnection())
            {
                return await _connection.QueryAsync<Account>("usp_Account_Sel", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Account> GetAsync(Guid guid)
        {
            using (var _connection = this.GetConnection())
            {
                var _parameters = new { guid };

                var _accounts = await _connection.QueryAsync<Account>("usp_Account_Sel", _parameters, commandType: System.Data.CommandType.StoredProcedure);

                return _accounts.FirstOrDefault();
            }
        }

        public async Task<int> PostAsync(Account value)
        {
            using (var _connection = this.GetConnection())
            {
                var _affected = await _connection.ExecuteAsync("usp_Account_Ins", value, commandType: System.Data.CommandType.StoredProcedure);
                return _affected;
            }
        }

        public async Task<int> PutAsync(Account value)
        {
            using (var _connection = this.GetConnection())
            {
                var _affected = await _connection.ExecuteAsync("usp_Account_Upd", value, commandType: System.Data.CommandType.StoredProcedure);
                return _affected;
            }
        }

        public async Task<int> DeleteAsync(Guid guid)
        {
            using (var _connection = this.GetConnection())
            {
                var _parameters = new { guid };

                var _affected = await _connection.ExecuteAsync("usp_Account_Del", _parameters, commandType: System.Data.CommandType.StoredProcedure);

                return _affected;
            }
        }
    }
}
