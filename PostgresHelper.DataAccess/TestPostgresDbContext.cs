using Microsoft.EntityFrameworkCore;
using PostgresHelper.DataAccess.Entities;

namespace PostgresHelper.DataAccess;

public class TestPostgresDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    
    public TestPostgresDbContext(DbContextOptions<TestPostgresDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}