using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.Extensions.Configuration;
using Repository;

namespace Application
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountApplication : ApplicationBase<AccountRepository>
    {
        public AccountApplication(IConfiguration configuration, AccountRepository repository) 
            : base(configuration, repository)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await this.Repository.GetAllAsync();
        }

        public async Task<Account> GetAsync(Guid guid)
        {
            return await this.Repository.GetAsync(guid);
        }

        public async Task PostAsync(Account value)
        {
            var _affected = await this.Repository.PostAsync(value);

            if (_affected <= 0)
                throw new Exception("Não foi possível salvar a Account!");
        }

        public async Task PutAsync(Guid guid, Account value)
        {
            var _account = await this.GetAsync(guid);

            if (_account == null)
                throw new Exception($"Account não encontrada para atualização! ID: {guid}");

            var _affected = await this.Repository.PutAsync(value);

            if (_affected <= 0)
                throw new Exception("Não foi possível atualizar a Account!");
        }

        public async Task DeleteAsync(Guid guid)
        {
            var _account = await this.GetAsync(guid);

            if (_account == null)
                throw new Exception($"Account não encontrada para exclusão! ID: {guid}");

            var _affected = await this.Repository.DeleteAsync(guid);

            if (_affected <= 0)
                throw new Exception("Não foi possível excluir a Account!");
        }
    }
}
