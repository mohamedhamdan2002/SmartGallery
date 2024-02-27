using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Server.Models;

namespace SmartGallery.Server.Data.ConFigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(i => i.Service)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.ServiceId)
            .IsRequired(true);

        builder.ToTable("Items");
    }
}
