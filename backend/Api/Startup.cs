﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Filters;
using Api.Infrastructure;
using Application.Entities.UserEntity.Command.SignUp;
using Application.Infrastructure.AutoMapper;
using Application.Infrastructure.RequestResponsePipeline;
using AutoMapper;
using BotDetect.Web;
using FluentValidation.AspNetCore;
using Infrastructure.Extensions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment ienv)
        {                           
            Configuration = configuration;  
            env = ienv;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var prod = Environment.GetEnvironmentVariable("DefaultConnection");
            var dev = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var connectionString = prod != null ? prod : dev;
            
            //  Configure Database and Microsoft Identity
            services.ConfigureDatabaseConnections(
                connectionString,
                "Api",
                env.IsStaging()
            );

            // Handling Application Exceptions on the Web
            services.AddMvc(options => {
                    options.Filters.Add(typeof(WebCustomExceptionFilter));
                })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            // For performing validation of user data before using in the application
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpValidator>());

            // Handle Model state errors
            services.AddScoped<IValidatorInterceptor, ValidatorInterceptor>();
     
             // Add DataContext implementation of Application interfaces
            services.ImplementApplicationDatabaseInterfaces();
            //
            // Add Infrastructure implementation of Application interfaces
            services.AddInfractureServices();

            // Add Api implementation of Application interfaces
            services.AddAPIServices();

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            
            //Add Mediator
            services.AddMediatR();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Add Swagger Open API
            if (env.IsDevelopment() || env.IsStaging())
            {
               services.AddSwaggerDocumentation();
            }

            // Check for JWT authentication where neccessary
            services.AddJwtAuthentication();

            services.AddMemoryCache(); // Adds a default in-memory  
                                       // implementation of  
                                       // IDistributedCache
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (env.IsDevelopment() || env.IsStaging()) {
                app.UseSwaggerDocumentation();
            }

            // Implement simple capcha
            app.UseSimpleCaptcha(Configuration.GetSection("BotDetect")); 


            // Make sure the database is created and the migration that was created is up to date..
            app.EnsureDatabaseAndMigrationsExtension();
            app.SeedLocationsToDatabase();

            // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
