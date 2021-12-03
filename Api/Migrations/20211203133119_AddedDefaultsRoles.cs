using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class AddedDefaultsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d770daeb-bfdf-479b-83c4-7b9f6ef23961", "5e7a46e6-dc44-4fe4-b973-eeda8a3939af", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "124b2aa8-1bee-4888-9db5-be9e2cbbcc9f", "888a7093-811d-4c1f-8f77-3d96d5f6e063", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "124b2aa8-1bee-4888-9db5-be9e2cbbcc9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d770daeb-bfdf-479b-83c4-7b9f6ef23961");
        }
    }
}
