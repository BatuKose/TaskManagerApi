using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApi.Migrations
{
    public partial class zimmetAlis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ZimmetOnayTarihi",
                table: "zımmetliKisiler",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "zimmetDurum",
                table: "zımmetliKisiler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZimmetOnayTarihi",
                table: "zımmetliKisiler");

            migrationBuilder.DropColumn(
                name: "zimmetDurum",
                table: "zımmetliKisiler");
        }
    }
}
