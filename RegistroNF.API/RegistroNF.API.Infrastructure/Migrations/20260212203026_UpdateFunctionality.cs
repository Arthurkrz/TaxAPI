using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroNF.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_EnderecoId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Empresas");

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_EmpresaId",
                table: "Enderecos",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Empresas_EmpresaId",
                table: "Enderecos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Empresas_EmpresaId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_EmpresaId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Empresas");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                table: "Empresas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EnderecoId",
                table: "Empresas",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
