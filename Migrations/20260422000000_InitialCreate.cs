using FleetRegistryService.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetRegistryService.Migrations;

[DbContext(typeof(FleetRegistryDbContext))]
[Migration("20260422000000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "fleet_registry");

        migrationBuilder.CreateTable(
            name: "captain",
            schema: "fleet_registry",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                rank = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_captain", captain => captain.id);
            });

        migrationBuilder.CreateTable(
            name: "spacecraft",
            schema: "fleet_registry",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                capacity = table.Column<int>(type: "integer", nullable: false),
                status = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_spacecraft", spacecraft => spacecraft.id);
                table.CheckConstraint("ck_spacecraft_capacity_non_negative", "capacity >= 0");
            });

        migrationBuilder.InsertData(
            schema: "fleet_registry",
            table: "captain",
            columns: new[] { "id", "name", "rank" },
            values: new object[,]
            {
                { 1, "Mara Voss", "Commander" },
                { 2, "Elias Renn", "Captain" },
                { 3, "Nia Okafor", "Senior Captain" }
            });

        migrationBuilder.InsertData(
            schema: "fleet_registry",
            table: "spacecraft",
            columns: new[] { "id", "capacity", "name", "status", "type" },
            values: new object[,]
            {
                { 1, 8, "Odyssey", "ACTIVE", "EXPLORER" },
                { 2, 3, "Asteria", "MAINTENANCE", "CARGO" },
                { 3, 12, "Vanguard", "ACTIVE", "SHUTTLE" },
                { 4, 6, "Kepler", "RETIRED", "RESEARCH" }
            });

        migrationBuilder.Sql("select setval(pg_get_serial_sequence('fleet_registry.captain', 'id'), 3, true);");
        migrationBuilder.Sql("select setval(pg_get_serial_sequence('fleet_registry.spacecraft', 'id'), 4, true);");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "captain",
            schema: "fleet_registry");

        migrationBuilder.DropTable(
            name: "spacecraft",
            schema: "fleet_registry");
    }
}
