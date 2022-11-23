using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.AuthorizationAPI.Migrations
{
    public partial class Updateuserentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4243b0af-6412-4681-9f89-6f381c6c2431");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "aa1dc2a3-2b80-4b8e-84d0-5477d2acdfa6");

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0fc5b095-096f-4a3a-be41-fad238b2a81a",
                column: "ConcurrencyStamp",
                value: "0fea3b1e-232e-40b7-82e6-d56ae40811b8");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ab479ce-921c-4601-bf8b-6dfa8aff6079", "87d69506-6891-4d4d-8c83-02a33fedb3e5", "Doctor", "DOCTOR" },
                    { "3cd1b32b-c1ed-4fdf-9970-e14a32d42c1a", "a412e6d0-b959-4593-86f9-31f3a8da5e13", "Patient", "PATIENT" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "31b9b272-53d0-4f6f-a190-0d5c70e242b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac8abc6d-adf0-4676-9cb9-26e36e337597", "AQAAAAEAACcQAAAAEEW7A1XOELiCiLvHOm9Db9mhZp7mGekduMiugfkGvQQ4tkiCtxNRwvoMK7oFhg12RQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0ab479ce-921c-4601-bf8b-6dfa8aff6079");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3cd1b32b-c1ed-4fdf-9970-e14a32d42c1a");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0fc5b095-096f-4a3a-be41-fad238b2a81a",
                column: "ConcurrencyStamp",
                value: "28687c30-4676-4b8c-a445-a5ce87dc8fb1");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4243b0af-6412-4681-9f89-6f381c6c2431", "638e42b3-87bd-44da-8a3b-c467514a2068", "Patient", "PATIENT" },
                    { "aa1dc2a3-2b80-4b8e-84d0-5477d2acdfa6", "3111b518-16b5-4c7f-ae03-23db5cf04bec", "Doctor", "DOCTOR" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "31b9b272-53d0-4f6f-a190-0d5c70e242b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "85c520cc-845c-453e-ab5e-0be8692332e0", "AQAAAAEAACcQAAAAEP+J8W1hQjkWRXQK83qLaH+kYHl9UXm9J5A+CiY1gRSjTdnS98IfiVadg8YuV68pAw==" });
        }
    }
}
