using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Asteria.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Vendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CodigoCliente = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    sku = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Faturamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");
        }
    }
}
