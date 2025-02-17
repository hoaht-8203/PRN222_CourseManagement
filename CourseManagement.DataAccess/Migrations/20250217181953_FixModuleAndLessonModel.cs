using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixModuleAndLessonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e1d2fe0f-9e1a-46c6-89b3-0d28fa3f26d7", "a425fb05-3b10-4722-afb4-a7c144fe2acd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3241193b-a48c-4587-b1aa-9dc79ade7984", "f1ec4ba7-9f74-4105-97c1-807f5f535bbd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3241193b-a48c-4587-b1aa-9dc79ade7984");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1d2fe0f-9e1a-46c6-89b3-0d28fa3f26d7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a425fb05-3b10-4722-afb4-a7c144fe2acd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1ec4ba7-9f74-4105-97c1-807f5f535bbd");

            migrationBuilder.DropColumn(
                name: "NextModuleId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "NextLessonId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "PreModuleId",
                table: "Modules",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "PrevLessonId",
                table: "Lessons",
                newName: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b2c104b2-9ea4-47d9-9dd3-c9098bfb4327", "1", "Admin", "ADMIN" },
                    { "dfa84aae-f28e-47d4-b403-d97c5510826a", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "072144ac-eb28-4e0f-883e-5af300f2cf10", 0, "b4246dc8-bc60-470f-8dee-a9e4ab96dc2d", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEI2ofqTaS/HOGL9CV6bsvkojqsVgMPyTkcgo1IKtf7Kn97cFJXIpyeVn9gL4gCVUJw==", null, false, "6129657d-678d-4c21-a7e8-53bc7d2108b1", false, "admin@admin.com" },
                    { "bf8d68fc-4e80-430e-8485-06737c5d59b9", 0, "d66f10ab-a001-462f-826c-0705d43b575a", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEEt0WioKvWq0yuwxq9sW8P6q3j9voR8G/tWMuKXZjTBLsC2xWAh/cvbRL65cBl5Icg==", null, false, "151cae56-1549-4f7b-bf71-c7ad8fe7b8d5", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b2c104b2-9ea4-47d9-9dd3-c9098bfb4327", "072144ac-eb28-4e0f-883e-5af300f2cf10" },
                    { "dfa84aae-f28e-47d4-b403-d97c5510826a", "bf8d68fc-4e80-430e-8485-06737c5d59b9" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b2c104b2-9ea4-47d9-9dd3-c9098bfb4327", "072144ac-eb28-4e0f-883e-5af300f2cf10" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dfa84aae-f28e-47d4-b403-d97c5510826a", "bf8d68fc-4e80-430e-8485-06737c5d59b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2c104b2-9ea4-47d9-9dd3-c9098bfb4327");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa84aae-f28e-47d4-b403-d97c5510826a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "072144ac-eb28-4e0f-883e-5af300f2cf10");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bf8d68fc-4e80-430e-8485-06737c5d59b9");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Modules",
                newName: "PreModuleId");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Lessons",
                newName: "PrevLessonId");

            migrationBuilder.AddColumn<int>(
                name: "NextModuleId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NextLessonId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3241193b-a48c-4587-b1aa-9dc79ade7984", "1", "Admin", "ADMIN" },
                    { "e1d2fe0f-9e1a-46c6-89b3-0d28fa3f26d7", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a425fb05-3b10-4722-afb4-a7c144fe2acd", 0, "5fa1a3ec-1a9b-443e-bcfe-015b8e60c5fa", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEPZ6KdZ4DKWUaw1Uyw9A7US05HrfAmBOPgpoVQgv1uA7CricaIePN1xhYEQjsSJ3aw==", null, false, "b7db5d62-1808-4488-8fbd-f32dc13b40c2", false, "user@user.com" },
                    { "f1ec4ba7-9f74-4105-97c1-807f5f535bbd", 0, "83a46b83-6055-4a95-8331-73ae44673304", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEIrxdoad7qbdzox0ry5YcTLMb/H5fLJp3FjYkSm0UqxysXJ6EiepvmtnhHp1JL53lA==", null, false, "bf2aac96-38e5-42aa-9119-892eee3c3353", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e1d2fe0f-9e1a-46c6-89b3-0d28fa3f26d7", "a425fb05-3b10-4722-afb4-a7c144fe2acd" },
                    { "3241193b-a48c-4587-b1aa-9dc79ade7984", "f1ec4ba7-9f74-4105-97c1-807f5f535bbd" }
                });
        }
    }
}
