using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolhaAPI.Migrations
{
    public partial class AddFgts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ImpostoFgts",
                table: "Folhas",
                type: "REAL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpostoFgts",
                table: "Folhas");
        }
    }
}
