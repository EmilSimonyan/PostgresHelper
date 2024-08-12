using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostgresHelper.PostgresNuget;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPostgresDatabase<TDatabase>(this IServiceCollection services,
            IConfiguration configuration,
            string databaseName,
            string migrationAssembly) where TDatabase : DbContext
        {
            return services
                .AddPostgresDatabase<TDatabase, BaseDataInitializationFilter<TDatabase>>(configuration, databaseName, migrationAssembly);
        }

    private static IServiceCollection AddPostgresDatabase<TDatabase, TMigrationFilter>(this IServiceCollection services,
            IConfiguration configuration,
            string databaseName,
            string migrationAssembly) where TDatabase : DbContext where TMigrationFilter : BaseDataInitializationFilter<TDatabase>
    {
        var connectionString = configuration.GetConnectionString(databaseName);
            services.AddDbContext<TDatabase>(options =>
                options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(migrationAssembly))
                .UseSnakeCaseNamingConvention());

            return services.AddTransient<IStartupFilter, TMigrationFilter>();
        }

    private static IServiceCollection AddPostgresDatabase<TDatabase, TMigrationFilter>(this IServiceCollection services,
            IConfiguration configuration) where TDatabase : DbContext where TMigrationFilter : BaseDataInitializationFilter<TDatabase>
        {
            var type = typeof(TDatabase);
            var migrationAssembly = type.Assembly.GetName().Name ?? throw new ArgumentException("Please provide assembly name");
            return services.AddPostgresDatabase<TDatabase, TMigrationFilter>(configuration, type.Name, migrationAssembly);
        }

        public static IServiceCollection AddPostgresDatabase<TDatabase>(this IServiceCollection services,
           IConfiguration configuration) where TDatabase : DbContext
        {
            return services
                .AddPostgresDatabase<TDatabase, BaseDataInitializationFilter<TDatabase>>(configuration);
        }
}