using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FolhaAPI.Migrations
{
    public partial class UpdateSalarioBruto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalarioBruno",
                table: "Folhas",
                newName: "SalarioBruto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalarioBruto",
                table: "Folhas",
                newName: "SalarioBruno");
        }
    }
}
