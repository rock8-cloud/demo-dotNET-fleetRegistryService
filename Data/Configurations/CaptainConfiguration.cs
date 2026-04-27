using FleetRegistryService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetRegistryService.Data.Configurations;

public sealed class CaptainConfiguration : IEntityTypeConfiguration<CaptainEntity>
{
    public void Configure(EntityTypeBuilder<CaptainEntity> entity)
    {
        entity.ToTable("captain");

        entity.HasKey(captain => captain.Id);

        entity.Property(captain => captain.Id)
            .HasColumnName("id");

        entity.Property(captain => captain.Name)
            .HasColumnName("name")
            .IsRequired();

        entity.Property(captain => captain.Rank)
            .HasColumnName("rank")
            .IsRequired();
    }
}
