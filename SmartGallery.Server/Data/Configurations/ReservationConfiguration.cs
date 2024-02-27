using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Server.Models;
using SmartGallery.Shared;

namespace SmartGallery.Server.Data.ConFigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReservationDate)
            .HasColumnType("date")
            .HasConversion(
                v => new DateTime(v.Year, v.Month, v.Day).Date,
                v => DateOnly.FromDateTime(v)
            );

        builder.Property(r => r.ReservationTime)
            .HasColumnType("time")
            .HasConversion(
                v => new TimeSpan(0, v.Hour, v.Minute, v.Millisecond),
                v => TimeOnly.FromDateTime(DateTime.MinValue.Add(v))
            );

        builder.Property(r => r.Status)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .HasConversion(
                v => v.ToString(),
                v => (StatusEnum) Enum.Parse(typeof(StatusEnum), v)
            );

        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.ServiceId)
            .IsRequired();

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .IsRequired();

        builder.HasOne(r => r.Item)
            .WithMany(i => i.Reservations)
            .HasForeignKey(r => r.ItemId)
            .IsRequired(false);

        builder.ToTable("Reservations");
    }
}
