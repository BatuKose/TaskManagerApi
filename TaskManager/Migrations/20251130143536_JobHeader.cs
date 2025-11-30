using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApi.Migrations
{
    public partial class JobHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobHeaders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "aktifMi",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobHeaders");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "aktifMi",
                value: false);
        }
    }
}
