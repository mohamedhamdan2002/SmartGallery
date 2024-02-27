using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SmartGallery.Server.Models;

namespace SmartGallery.Server.Data;

public class AppDbContext:IdentityDbContext<Customer>
{
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Item> Items { get; set; }
	public DbSet<Review> Reviews { get; set; }
    public AppDbContext(DbContextOptions options) :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER",
            Id = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),

        });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = "1",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1", RoleId = "1" } 
        );
        modelBuilder.Entity<Customer>().HasData(InitializeAdmin());
    }
    public static Customer InitializeAdmin()
    {
        var user = new Customer
        {
            Id = "1",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            PhoneNumber = "01018004723",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };
        var password = new PasswordHasher<Customer>();
        var hashed = password.HashPassword(user, "Admin123!");
        user.PasswordHash = hashed;
        return user;
    }
}

