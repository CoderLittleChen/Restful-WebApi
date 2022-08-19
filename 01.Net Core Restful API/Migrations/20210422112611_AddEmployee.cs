using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.Net_Core_Restful_API.Migrations
{
    public partial class AddEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("07389c3c-ac3e-46f9-b6bd-5320b7eb326d"), new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "cm20210001", "Nick", 1, "Brand" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("5e23cc44-50f3-4ef7-976b-92e0991899b8"), new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "cm20210002", "Vince", 2, "John" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("a9061730-7868-4869-9f96-f17d5ac97690"), new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "cm20210003", "Bryant", 1, "Kobbo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("07389c3c-ac3e-46f9-b6bd-5320b7eb326d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("5e23cc44-50f3-4ef7-976b-92e0991899b8"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a9061730-7868-4869-9f96-f17d5ac97690"));
        }
    }
}
