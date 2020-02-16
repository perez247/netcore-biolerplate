using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence.Extensions
{
    public static class IdentityExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services) {
            IdentityBuilder builder = services.AddIdentityCore<User>(opts => {
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.MaxFailedAccessAttempts = 10;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opts.User.RequireUniqueEmail = true;
                opts.Lockout.AllowedForNewUsers = false;
            });
            // .AddUserValidator<UniqueEmail<User>>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DefaultDataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();

            // services.Configure<IdentityOptions>(options =>
            // {
            //     // Default Lockout settings.
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            //     options.Lockout.MaxFailedAccessAttempts = 5;
            //     options.Lockout.AllowedForNewUsers = true;
            //     options.SignIn.RequireConfirmedEmail = true;
            //     options.User.RequireUniqueEmail = true;
            // });

            // Add cokkies to the application the only resason im using this is for lockout attempt
            services.AddAuthentication().AddApplicationCookie();
        }
    }
}