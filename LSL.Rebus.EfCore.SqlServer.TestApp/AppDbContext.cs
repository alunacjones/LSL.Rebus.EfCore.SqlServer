using Microsoft.EntityFrameworkCore;
using LSL.Rebus.EfCore.SqlServer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddRebusSagaTablesForSqlServer();
    }
}
