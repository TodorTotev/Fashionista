namespace Fashionista.Web.Infrastructure
{
    using System;

    using Fashionista.Application;
    using Fashionista.Domain.Entities;
    using Fashionista.Infrastructure.Humanizer;
    using Fashionista.Persistence;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureIdentity(
            this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureSession(
            this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = new TimeSpan(0, 4, 0, 0);
                options.Cookie.IsEssential = true;
            });

            return services;
        }

        public static IServiceCollection ConfigureApplicationCookie(
            this IServiceCollection services)
        {
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            return services;
        }

        public static IServiceCollection ConfigureCookiePolicy(
            this IServiceCollection services)
        {
            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });

            return services;
        }

        public static IServiceCollection ConfigureControllers(
            this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    o.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                })
                .AddMvcOptions(m => m.ModelMetadataDetailsProviders.Add(new HumanizerMetadataProvider()))
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<ApplicationDependencyInjectionHelper>());

            return services;
        }
    }
}
