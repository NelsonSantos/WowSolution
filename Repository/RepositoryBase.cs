using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Repository
{
    public abstract class RepositoryBase
    {
        public IConfiguration Configuration { get; }

        protected RepositoryBase(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(this.ConnectionString);
        }

        public string ConnectionString { get => this.Configuration["ConnectionString"]; }
    }
}
