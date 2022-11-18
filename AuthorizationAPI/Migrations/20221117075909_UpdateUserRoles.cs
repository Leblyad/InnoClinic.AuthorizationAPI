using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizationAPI.Migrations
{
    public partial class UpdateUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "34b20ce2-8ae8-47ce-842e-ae864d03e5f1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5ff5bff3-c738-4584-8472-3c7dcfcacabe");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04965986-8f9f-4bf6-a1c5-db3bc65ebab3", "407781f9-0621-4e22-81ca-b96b4ff684a4", "Pacient", "PACIENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "124a1ac8-5be2-4c91-b7a5-109620464541", "a11be975-30c9-42a9-b75e-3431350e11f7", "Receptionist", "RECEPTIONIST" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e6a508d-9317-4358-b9bf-00b54486cc9f", "5930822d-c601-4cbd-89aa-0c66be703530", "Doctor", "DOCTOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "04965986-8f9f-4bf6-a1c5-db3bc65ebab3");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "124a1ac8-5be2-4c91-b7a5-109620464541");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4e6a508d-9317-4358-b9bf-00b54486cc9f");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34b20ce2-8ae8-47ce-842e-ae864d03e5f1", "27e38306-ce95-4f7f-ab47-eef491639a27", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ff5bff3-c738-4584-8472-3c7dcfcacabe", "25ed0436-171b-4d9f-907a-f73906c06237", "Manager", "MANAGER" });
        }
    }
}
