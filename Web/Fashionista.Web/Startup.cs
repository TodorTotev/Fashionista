using Fashionista.Web.Middlewares;

namespace Fashionista.Web
{
    using AutoMapper;
    using Fashionista.Application;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Infrastructure;
    using Fashionista.Infrastructure.Cloudinary;
    using Fashionista.Infrastructure.Hubs;
    using Fashionista.Infrastructure.Messaging;
    using Fashionista.Persistence;
    using Fashionista.Persistence.Infrastructure;
    using Fashionista.Persistence.Interfaces;
    using Fashionista.Persistence.Repositories;
    using Fashionista.Persistence.Seeding;
    using Fashionista.Web.Infrastructure;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Framework services
            // TODO: Add pooling when this bug is fixed: https://github.com/aspnet/EntityFrameworkCore/issues/9741
            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureIdentity();
            services.ConfigureApplicationCookie();
            services.ConfigureCookiePolicy();
            services.ConfigureSession();
            services.AddRazorPages();
            services.ConfigureControllers();
            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(ApplicationDependencyInjectionHelper).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddSingleton(this.configuration);

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = this.configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = this.configuration["Authentication:Google:ClientSecret"];
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];
            });

            // Identity stores
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();

            services.AddSingleton(x => CloudinaryFactory.GetInstance(this.configuration));
            services.AddTransient<ICloudinaryHelper, CloudinaryHelper>();
            services.AddScoped<IUserAssistant, UserAssistant>();
            services.AddScoped<IShoppingCartAssistant, ShoppingCartAssistant>();

            services.Configure<StripeSettings>(this.configuration.GetSection("Stripe"));

            services.Configure<RedisConfigurationOptions>(this.configuration.GetSection("Redis"));
            services.AddTransient(typeof(IRedisService<>), typeof(RedisService<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder()
                    .SeedAsync(dbContext, serviceScope.ServiceProvider)
                    .GetAwaiter()
                    .GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            StripeConfiguration.SetApiKey(this.configuration.GetSection("Stripe")["SecretKey"]);

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNotificationHandlerMiddleware();
            app.UseCustomExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserNotificationHub>("/userNotificationHub");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
