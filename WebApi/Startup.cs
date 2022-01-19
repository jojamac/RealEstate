using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddApplicationLayer();
            services.AddContextInfrastructure(_configuration);
            services.AddPersistence(_configuration);
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddEssentials();
            services.AddControllers();

            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Apps/rem/build";
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            

            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


          


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Apps/rem";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
