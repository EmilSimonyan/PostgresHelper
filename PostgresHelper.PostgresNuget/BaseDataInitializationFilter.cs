using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace PostgresHelper.PostgresNuget;

public class BaseDataInitializationFilter<TDatabase> : IStartupFilter where TDatabase : DbContext
{
    public virtual Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            MigrateDatabase(builder);
            next(builder);
        };
    }
    
    protected virtual void MigrateDatabase(IApplicationBuilder builder, bool resetDatabase = false)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDatabase>();

        if (resetDatabase)
            dbContext.Database.EnsureDeleted();

        dbContext.Database.Migrate();

        if (dbContext.Database.GetDbConnection() is not NpgsqlConnection connection)
            return;
        
        connection.OpenAsync().Wait();
        connection.ReloadTypes();
    }

}