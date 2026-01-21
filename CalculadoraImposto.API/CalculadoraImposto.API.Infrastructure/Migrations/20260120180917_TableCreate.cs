using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalculadoraImposto.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Impostos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorImposto = table.Column<double>(type: "float", nullable: false),
                    Aliquota = table.Column<double>(type: "float", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LucroPresumido = table.Column<double>(type: "float", nullable: false),
                    AnoReferencia = table.Column<int>(type: "int", nullable: false),
                    MesReferencia = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImpostoEmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impostos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Impostos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Impostos_Empresas_ImpostoEmpresaId",
                        column: x => x.ImpostoEmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    NotaFiscalEmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Empresas_NotaFiscalEmpresaId",
                        column: x => x.NotaFiscalEmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "ID");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Impostos_EmpresaID",
                table: "Impostos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Impostos_ImpostoEmpresaId",
                table: "Impostos",
                column: "ImpostoEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaID",
                table: "NotasFiscais",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_NotaFiscalEmpresaId",
                table: "NotasFiscais",
                column: "NotaFiscalEmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Impostos");

            migrationBuilder.DropTable(
                name: "NotasFiscais");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
