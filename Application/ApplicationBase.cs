using Microsoft.Extensions.Configuration;
using Repository;
using System;

namespace Application
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApplicationBase<T> 
        where T: RepositoryBase
    {
        public T Repository { get; }
        public IConfiguration Configuration { get; }
        protected ApplicationBase(IConfiguration configuration, T repository)
        {
            this.Repository = repository;
            this.Configuration = configuration;
        }
    }
}
