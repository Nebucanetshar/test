using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace grpc;

public class AppDbContext:DbContext
{
    protected readonly IConfiguration configuration;
    
    public AppDbContext (IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(configuration.GetConnectionString("MergeBase"));
    }
    public DbSet<Counter> counter { get; set; }
}
