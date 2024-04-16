using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Minha_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    Apelido = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "Apelido", "Ativo", "Cadastro", "Name" },
                values: new object[] { 1, "Cafe", true, new DateTime(2024, 4, 15, 21, 27, 31, 445, DateTimeKind.Local).AddTicks(4990), "Robson" });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "Apelido", "Ativo", "Cadastro", "Name" },
                values: new object[] { 2, null, false, new DateTime(2024, 4, 15, 21, 27, 31, 453, DateTimeKind.Local).AddTicks(6221), "Carlo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItem");
        }
    }
}
