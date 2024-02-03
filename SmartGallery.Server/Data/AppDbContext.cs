using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Models;

namespace SmartGallery.Server.Data;

public class AppDbContext:IdentityDbContext<Customer>
{
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
	public AppDbContext(DbContextOptions options) :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

