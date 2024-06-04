using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Core.Interfaces;
using Plugin.Core.Services;
using Plugin.Repository.DbContexts;
using Plugin.Repository.Repositories;
using System.Reflection;

namespace Plugin.Core.Configurations
{
    public static class PluginCoreConfigurations
    {
        private readonly static Assembly _assemblyInfo = typeof(PluginCoreConfigurations).Assembly;
        public static IServiceCollection ConfigurePluginCore(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });

            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IImageService, ImageService>();

            services.AddAutoMapper(_assemblyInfo);

            return services;
        }
    }
}
