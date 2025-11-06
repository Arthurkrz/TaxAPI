using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroNF.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUFColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UF",
                table: "Enderecos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UF",
                table: "Enderecos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
