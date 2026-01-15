using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroNF.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUniqueIndexNF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_Serie_Numero",
                table: "NotasFiscais");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_Serie_Numero_EmpresaId",
                table: "NotasFiscais",
                columns: new[] { "Serie", "Numero", "EmpresaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_Serie_Numero_EmpresaId",
                table: "NotasFiscais");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_Serie_Numero",
                table: "NotasFiscais",
                columns: new[] { "Serie", "Numero" },
                unique: true);
        }
    }
}
