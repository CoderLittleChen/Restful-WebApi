using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.Net_Core_Restful_API.Migrations
{
    public partial class AddCompanyBankruptTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BankruptTime",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                column: "Introduction",
                value: "Great Company");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankruptTime",
                table: "Companies");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                column: "Introduction",
                value: "Great Compant");
        }
    }
}
