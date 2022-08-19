using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.Net_Core_Restful_API.Migrations
{
    public partial class AddCompanyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1a805447-13db-41c8-861e-370942215b96"),
                columns: new[] { "Country", "Industry", "Introduction", "Product" },
                values: new object[] { "China", "Mobile Phone", "Mobile Phone Company", "Xiao Mi 11" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("423d5d98-a496-44d8-8008-bc0b9c40fd80"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "China", "Live", "Douyu Live" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("431c949b-7b83-46ac-943b-d4e773495041"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "China", "Delivery", "Mei Tuan App" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5001e311-51d2-469f-8446-ba5364155f29"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "America", "PC System", "Office 365" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("50d80f33-844d-4d28-9504-7e541719ff36"),
                columns: new[] { "Country", "Industry", "Introduction", "Product" },
                values: new object[] { "China", "Social APP,Game", "Game Company", "WeChat,QQ" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5313d3dc-874c-4b46-a8b8-2018923213fb"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "China", "Internet", "AliPay" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "America", "Android", "Google Chrome Browser" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("b230f808-1cad-47ea-a484-68ccb3d53154"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "America", "Social", "Twitter" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("b3a3a73e-88c0-4c94-af92-c9d11b0a8b9c"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "China", "Short Video", "Tik Tok" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f322d130-6e24-477e-8f49-684bfc681211"),
                columns: new[] { "Country", "Industry", "Introduction", "Product" },
                values: new object[] { "China", "Safe Software", "Software Company", "360" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f337a186-d3f0-4809-b2e7-f9370b129e61"),
                columns: new[] { "Country", "Industry", "Introduction", "Product" },
                values: new object[] { "America", "Rocket,Electric Car", "Rocket,Electric Car", "Tesla" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Companies");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1a805447-13db-41c8-861e-370942215b96"),
                column: "Introduction",
                value: "MI Phone");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("50d80f33-844d-4d28-9504-7e541719ff36"),
                column: "Introduction",
                value: "QQ");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f322d130-6e24-477e-8f49-684bfc681211"),
                column: "Introduction",
                value: "Software");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f337a186-d3f0-4809-b2e7-f9370b129e61"),
                column: "Introduction",
                value: "Rocket");
        }
    }
}
