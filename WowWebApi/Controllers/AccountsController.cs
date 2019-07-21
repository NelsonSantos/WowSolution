using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository;

namespace WowWebApi.Controllers
{
    /// <summary>
    /// API responsável pelo controle de Account's
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<AccountApplication, AccountRepository>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="application"></param>
        public AccountsController(IConfiguration configuration, AccountApplication application) 
            : base(configuration, application)
        {
        }

        /// <summary>
        /// Retorna uma lista de Account's cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await this.Application.GetAllAsync();
        }

        /// <summary>
        /// Retorna uma Account pelo seu Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        public async Task<Account> GetAsync(Guid guid)
        {
            return await this.Application.GetAsync(guid);
        }

        /// <summary>
        /// Insere uma nova Account
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task PostAsync([FromBody] Account value)
        {
            await this.Application.PostAsync(value);
        }

        /// <summary>
        /// Atualiza os dados de uma Account de acordo com Guid fornecido
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="value"></param>
        [HttpPut("{guid}")]
        public async Task PutAsync(Guid guid, [FromBody] Account value)
        {
            await this.Application.PutAsync(guid, value);
        }

        /// <summary>
        /// Exclui uma Account de acordo com Guid fornecido
        /// </summary>
        /// <param name="guid"></param>
        [HttpDelete("{guid}")]
        public async Task DeleteAsync(Guid guid)
        {
            await this.Application.DeleteAsync(guid);
        }
    }
}
