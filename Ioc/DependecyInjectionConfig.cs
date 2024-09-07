using ConwayGameOfLife.Business.Interfaces;
using ConwayGameOfLife.Business.Managers;
using ConwayGameOfLife.Business.Services;
using ConwayGameOfLife.Business.Services.Interfaces;
using ConwayGameOfLife.Infra.Data.Context;
using ConwayGameOfLife.Infra.Data.Interfaces;
using ConwayGameOfLife.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ioc
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string defaultConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<GameOfLifeDbContext>(options =>options.UseSqlite(defaultConnection, b => b.MigrationsAssembly("ConwayGameOfLife.Infra.Data")));

            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IBoardManager, BoardManager>();
            services.AddScoped<IBoardRepository, BoardRepository>();

            return services;
        }
    }
}