using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalculadoraImposto.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impostos_Empresas_EmpresaID",
                table: "Impostos");

            migrationBuilder.DropForeignKey(
                name: "FK_Impostos_Empresas_ImpostoEmpresaId",
                table: "Impostos");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaID",
                table: "NotasFiscais");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_NotaFiscalEmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_NotaFiscalEmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_Impostos_ImpostoEmpresaId",
                table: "Impostos");

            migrationBuilder.DropColumn(
                name: "NotaFiscalEmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "ImpostoEmpresaId",
                table: "Impostos");

            migrationBuilder.RenameColumn(
                name: "EmpresaID",
                table: "NotasFiscais",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaID",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaId");

            migrationBuilder.RenameColumn(
                name: "EmpresaID",
                table: "Impostos",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Impostos_EmpresaID",
                table: "Impostos",
                newName: "IX_Impostos_EmpresaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "NotasFiscais",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Impostos_Empresas_EmpresaId",
                table: "Impostos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaId",
                table: "NotasFiscais",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impostos_Empresas_EmpresaId",
                table: "Impostos");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaId",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "NotasFiscais",
                newName: "EmpresaID");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaId",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaID");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Impostos",
                newName: "EmpresaID");

            migrationBuilder.RenameIndex(
                name: "IX_Impostos_EmpresaId",
                table: "Impostos",
                newName: "IX_Impostos_EmpresaID");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaID",
                table: "NotasFiscais",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "NotaFiscalEmpresaId",
                table: "NotasFiscais",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImpostoEmpresaId",
                table: "Impostos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_NotaFiscalEmpresaId",
                table: "NotasFiscais",
                column: "NotaFiscalEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Impostos_ImpostoEmpresaId",
                table: "Impostos",
                column: "ImpostoEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Impostos_Empresas_EmpresaID",
                table: "Impostos",
                column: "EmpresaID",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Impostos_Empresas_ImpostoEmpresaId",
                table: "Impostos",
                column: "ImpostoEmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaID",
                table: "NotasFiscais",
                column: "EmpresaID",
                principalTable: "Empresas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_NotaFiscalEmpresaId",
                table: "NotasFiscais",
                column: "NotaFiscalEmpresaId",
                principalTable: "Empresas",
                principalColumn: "ID");
        }
    }
}
