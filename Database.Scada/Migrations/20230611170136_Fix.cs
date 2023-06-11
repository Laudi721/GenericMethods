using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.GenericMethods.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contractors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contractors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
