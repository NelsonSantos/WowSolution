using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using Swashbuckle.AspNetCore.Swagger;
using WowWebApi.Filters;

namespace WowWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private const string SwaggerName = "Wow Web Api Docs";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                // centralizando o tratamento de erros, gravando logs e ou ApplicationInsights
                config.Filters.Add(typeof(ErrorFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = SwaggerName, Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments("WowWebApi.xml");
            });

            services.AddScoped<AccountRepository>();
            services.AddScoped<AccountApplication>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/help";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{SwaggerName} versão 1.0");
                c.DocumentTitle = SwaggerName;
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
