using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba8b7478-8d48-4138-92c2-ba0a5a0ac2a6", "AQAAAAIAAYagAAAAEA4sEi5fifLNicFmrHv5lW887JwSX5dK0NuBwFL6wqftHy3rOWDtnRtAE+XRhBiWTw==", "912c9ac0-8f05-4257-b99f-659c040b5629" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a6d761d-d2eb-426f-8aeb-017935f7f8c7", "AQAAAAIAAYagAAAAELxXvy+UOrQpRmy+GjizsQCdkkRJAbt1OifLo+zbcQYMsFwLD391lQayBnBJD07bPw==", "9f0aec79-b071-434d-a10e-91dc5569e21c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Documents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "635375f0-6395-4c59-b31e-3ae026f855c3", "AQAAAAIAAYagAAAAEKs+mDAleHj0nNqbni8g+A3jKxTOsDWcjiZ9cQPzYZR9jNf73wz65VPZHrQO9rBAJg==", "01623f11-e2cf-4f6d-abe6-b3f7efec7d5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56f2b3e9-17ea-48a4-bc60-2213a6723d08", "AQAAAAIAAYagAAAAEGo5l6MmBdEw8EShx5nzxdfERbEDgoiGtJxjnNJsGstrXJazCnKccVNyHfMmjJXsTg==", "4e7f0ef4-3626-47f6-b751-aab4655e1e84" });
        }
    }
}
