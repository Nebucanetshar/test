using Microsoft.EntityFrameworkCore;
using gRpc;


namespace grpc.Data;

public class AppDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("backTest"));
    }
    public DbSet<Items> Items { get; set; }
}
