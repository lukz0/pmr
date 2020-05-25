using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace backend.Migrations
{
    public partial class MissionQueueRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionQueueRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionId = table.Column<int>(nullable: false),
                    RobotId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Guid = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionQueueRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionQueueRequests_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionQueueRequests_Robots_RobotId",
                        column: x => x.RobotId,
                        principalTable: "Robots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionQueuesResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    RobotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionQueuesResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionQueuesResponse_Robots_RobotId",
                        column: x => x.RobotId,
                        principalTable: "Robots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionQueueRequests_MissionId",
                table: "MissionQueueRequests",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionQueueRequests_RobotId",
                table: "MissionQueueRequests",
                column: "RobotId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionQueuesResponse_RobotId",
                table: "MissionQueuesResponse",
                column: "RobotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionQueueRequests");

            migrationBuilder.DropTable(
                name: "MissionQueuesResponse");
        }
    }
}
