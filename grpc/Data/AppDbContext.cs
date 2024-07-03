﻿using grpc.Model;
using Microsoft.EntityFrameworkCore;

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
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("pgsql"));
    }
    public DbSet<Items> Items { get; set; }
}
