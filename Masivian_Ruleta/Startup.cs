using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masivian_Ruleta.Business;
using Masivian_Ruleta.Business.Interface;
using Masivian_Ruleta.Repository;
using Masivian_Ruleta.Repository.Interface;
using MasivianRuleta.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MasivianApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            Configuration.Bind("AppConfiguration", AppConfiguration.Instance);
            AddSwagger(services);

            #region Repository Extension
            services.AddTransient<IRuletaRepository, RuletaRepository>();
            #endregion

            #region Business Extension
            services.AddTransient<IRuletaBusiness, RuletaBusiness>();
            #endregion
        }

        /// <summary>
        /// Configuration of swagger
        /// </summary>
        /// <param name="services"></param>
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"MASIVIAN API {groupName}",
                    Version = groupName,
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Masivian",
                        Email = string.Empty
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masivian API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
