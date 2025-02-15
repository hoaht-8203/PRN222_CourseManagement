using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixTableLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "daafbb51-7b07-4bcc-910f-112824294724", "2eea696b-761b-4cde-9cad-4025e5fe11c0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ca0ad912-c58a-4091-add8-8bcb9e711446", "4528b912-0717-4253-9262-516237e8ff11" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca0ad912-c58a-4091-add8-8bcb9e711446");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daafbb51-7b07-4bcc-910f-112824294724");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2eea696b-761b-4cde-9cad-4025e5fe11c0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4528b912-0717-4253-9262-516237e8ff11");

            migrationBuilder.AddColumn<int>(
                name: "NextLessonId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevLessonId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "NextLessonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "PrevLessonId",
                table: "Lessons");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca0ad912-c58a-4091-add8-8bcb9e711446", "1", "Admin", "ADMIN" },
                    { "daafbb51-7b07-4bcc-910f-112824294724", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2eea696b-761b-4cde-9cad-4025e5fe11c0", 0, "01109bfd-640d-4381-b4ee-30764d98173c", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEOvp48svxJLH21ZDbcYcbHBEXj8L4tktK/eoSkZ0WwRWk7u1hy6uc84kqU3i2vu5YA==", null, false, "24d0eb1f-fc41-4d83-9d19-d842cf5b3129", false, "user@user.com" },
                    { "4528b912-0717-4253-9262-516237e8ff11", 0, "85a4c523-78a6-4b66-b492-c3e2e9610341", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEIUzbpbkCWgbJkMBU6Vz8ldunt8pwlj+7L7O3ecGIaLFfy1bjd3gbCprSKqUb3M65Q==", null, false, "84296fbc-f99e-406a-a81d-ce777463919c", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "daafbb51-7b07-4bcc-910f-112824294724", "2eea696b-761b-4cde-9cad-4025e5fe11c0" },
                    { "ca0ad912-c58a-4091-add8-8bcb9e711446", "4528b912-0717-4253-9262-516237e8ff11" }
                });
        }
    }
}
