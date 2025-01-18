using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using grpc.Models;

namespace grpc;

public class AppDbContext:DbContext
{
    protected readonly IConfiguration _configuration;
    public DbSet<Items> Items { get; set; }
    public DbSet<CounterState> State { get; set; }

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
  
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("vans"));
    }

}
