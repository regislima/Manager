using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Manager.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "VARCHAR(180)", maxLength: 180, nullable: false),
                    password = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    createdat = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    updatedat = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
