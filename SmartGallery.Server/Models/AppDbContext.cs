using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartGallery.Server.Models;

public class AppDbContext:IdentityDbContext
{ 
	public AppDbContext(DbContextOptions options) :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public DbSet<Employee> Employees { get; set; }
}

