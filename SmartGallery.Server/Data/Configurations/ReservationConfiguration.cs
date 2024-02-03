using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Server.Models;

namespace SmartGallery.Server.Data.ConFigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(s => new { s.CustomerId, s.ServiceId });

        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.ServiceId)
            .IsRequired();

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .IsRequired();

        builder.ToTable("Reservations");
    }
}
