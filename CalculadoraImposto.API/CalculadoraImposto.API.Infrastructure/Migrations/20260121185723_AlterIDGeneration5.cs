using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalculadoraImposto.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterIDGeneration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Empresas",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_NotaFiscalEmpresaId",
                table: "NotasFiscais",
                column: "NotaFiscalEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Impostos_ImpostoEmpresaId",
                table: "Impostos",
                column: "ImpostoEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CNPJ",
                table: "Empresas",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_RazaoSocial",
                table: "Empresas",
                column: "RazaoSocial",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Impostos_Empresas_ImpostoEmpresaId",
                table: "Impostos",
                column: "ImpostoEmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_NotaFiscalEmpresaId",
                table: "NotasFiscais",
                column: "NotaFiscalEmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impostos_Empresas_ImpostoEmpresaId",
                table: "Impostos");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_NotaFiscalEmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_NotaFiscalEmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_Impostos_ImpostoEmpresaId",
                table: "Impostos");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_CNPJ",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_RazaoSocial",
                table: "Empresas");

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }
    }
}
