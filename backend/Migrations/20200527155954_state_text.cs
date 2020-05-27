using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class state_text : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateText",
                table: "Robots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateText",
                table: "Robots");
        }
    }
}
