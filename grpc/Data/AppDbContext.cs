using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace grpc;

public class AppDbContext:DbContext
{
    protected readonly IConfiguration _configuration;
    public DbSet<Items> Items { get; set; }

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("vans"));
    }

    internal async Task SaveChangesAsync(List<Items> items) { }
    
       
    
}
