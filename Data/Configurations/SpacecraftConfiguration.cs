using FleetRegistryService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetRegistryService.Data.Configurations;

public sealed class SpacecraftConfiguration : IEntityTypeConfiguration<SpacecraftEntity>
{
    public void Configure(EntityTypeBuilder<SpacecraftEntity> entity)
    {
        entity.ToTable("spacecraft");

        entity.HasKey(spacecraft => spacecraft.Id);

        entity.Property(spacecraft => spacecraft.Id)
            .HasColumnName("id");

        entity.Property(spacecraft => spacecraft.Name)
            .HasColumnName("name")
            .IsRequired();

        entity.Property(spacecraft => spacecraft.Type)
            .HasColumnName("type")
            .IsRequired();

        entity.Property(spacecraft => spacecraft.Capacity)
            .HasColumnName("capacity")
            .IsRequired();

        entity.Property(spacecraft => spacecraft.Status)
            .HasColumnName("status")
            .IsRequired();

        entity.HasCheckConstraint("ck_spacecraft_capacity_non_negative", "capacity >= 0");
    }
}
