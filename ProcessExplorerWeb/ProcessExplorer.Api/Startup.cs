using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessExplorer.Api.Filters;
using ProcessExplorer.Api.Services;
using ProcessExplorer.Api.SignalR;
using ProcessExplorer.Api.Soap.Interfaces;
using ProcessExplorerWeb.Application;
using ProcessExplorerWeb.Application.Extensions;
using ProcessExplorerWeb.Infrastructure;
using SoapCore;
using System.Reflection;
using System.ServiceModel;

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
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddSoapCore();
            services.AddHealthChecks().AddCheck<SqlHealthCheckService>(nameof(SqlHealthCheckService));

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
                //REST api
                endpoints.MapControllers();

                //SignalR (sockets)
                endpoints.MapHub<ProcessExplorerHub>("/processhub");

                //Soap
                endpoints.UseSoapEndpoint<ISyncService>("/SyncService.asmx", new BasicHttpBinding());

                //health check
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
