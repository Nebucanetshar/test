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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ////configuration de la relation entre Items et CounterState 
        //modelBuilder.Entity<Items>()
        //    .HasOne(e => e.CurrentCount)
        //    .WithOne()
        //    .HasForeignKey<Items>(e => e.Id);

        //configuration de l'entity CounterState sans clé primaire 
        modelBuilder.Entity<CounterState>().HasNoKey();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("vans"));
    }

}
