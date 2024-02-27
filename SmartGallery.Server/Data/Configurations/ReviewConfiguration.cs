using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Server.Models;

namespace SmartGallery.Server.Data.ConFigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.ServiceId)
            .IsRequired();

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .IsRequired();

        builder.ToTable("Reviews");
    }
}