using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace backend.Migrations
{
    public partial class missionQueueResponseComposite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionQueuesResponse",
                table: "MissionQueuesResponse");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MissionQueuesResponse",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionQueuesResponse",
                table: "MissionQueuesResponse",
                columns: new[] { "Id", "RobotId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionQueuesResponse",
                table: "MissionQueuesResponse");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MissionQueuesResponse",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionQueuesResponse",
                table: "MissionQueuesResponse",
                column: "Id");
        }
    }
}
