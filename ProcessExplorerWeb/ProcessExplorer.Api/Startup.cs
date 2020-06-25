using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessExplorer.Api.Filters;
using ProcessExplorer.Api.SignalR;
using ProcessExplorerWeb.Application;
using ProcessExplorerWeb.Application.Extensions;
using ProcessExplorerWeb.Infrastructure;

namespace ProcessExplorer.Api
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
            services.ConfigureIInstallationCofigurations(Configuration, Assembly.GetExecutingAssembly());
            services.AddInfrastructureLayer(Configuration);
            services.AddApplicationLayer(Configuration);
            services.AddHttpContextAccessor();
            services.AddControllers(opt => opt.Filters.Add(new ExceptionFilter()));

            services.AddCors(options =>
            {
                options.AddPolicy("ProcessExplorerPolicy", builder =>
                {
                    builder.WithOrigins(new string[] { "https://processexplorerfront.azurewebsites.net", "http://localhost:4200" });
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Process Explorer API V1");
                });
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Process Explorer API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("ProcessExplorerPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ProcessExplorerHub>("/processhub");
            });
        }
    }
}
