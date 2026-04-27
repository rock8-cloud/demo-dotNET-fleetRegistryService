using FleetRegistryService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetRegistryService.Migrations;

[DbContext(typeof(FleetRegistryDbContext))]
partial class FleetRegistryDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("fleet_registry")
            .HasAnnotation("ProductVersion", "8.0.4")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("FleetRegistryService.Data.Entities.CaptainEntity", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer")
                .HasColumnName("id");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(entity.Property<int>("Id"));

            entity.Property<string>("Name")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("name");

            entity.Property<string>("Rank")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("rank");

            entity.HasKey("Id");

            entity.ToTable("captain", "fleet_registry");
        });

        modelBuilder.Entity("FleetRegistryService.Data.Entities.SpacecraftEntity", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer")
                .HasColumnName("id");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(entity.Property<int>("Id"));

            entity.Property<int>("Capacity")
                .HasColumnType("integer")
                .HasColumnName("capacity");

            entity.Property<string>("Name")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("name");

            entity.Property<string>("Status")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("status");

            entity.Property<string>("Type")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("type");

            entity.HasKey("Id");

            entity.ToTable("spacecraft", "fleet_registry", table =>
            {
                table.HasCheckConstraint("ck_spacecraft_capacity_non_negative", "capacity >= 0");
            });
        });
    }
}
