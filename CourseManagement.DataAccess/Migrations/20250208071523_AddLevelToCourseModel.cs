using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelToCourseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "28baaeb0-6499-451d-bbb8-f143ebd7ef79", "194b47e7-c27b-47ae-801b-61521ddfbee0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0dc2da46-1f7f-4896-85e7-adda07a196fd", "45b06f22-a224-449f-9f30-b5224b330f6c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dc2da46-1f7f-4896-85e7-adda07a196fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28baaeb0-6499-451d-bbb8-f143ebd7ef79");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "194b47e7-c27b-47ae-801b-61521ddfbee0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45b06f22-a224-449f-9f30-b5224b330f6c");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b7fc3d0-e918-4996-9621-e29575a2c653", "1", "Admin", "ADMIN" },
                    { "698ec717-470c-43d9-8152-6ceb66d87d0d", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a64cea32-50dc-48ae-961a-2b4c46c195f2", 0, "7231333e-0ac6-4ebb-88c4-18279c67a528", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEGP0Crfgoos3STS/Bx1lfWu/h9orxOifvYsvN7qwhgh3xqc7YlDWAVQrMMscRmjk+w==", null, false, "2ebb5cf7-3351-450f-9439-b841bc828e99", false, "user@user.com" },
                    { "f4c1fcbe-0de2-479a-b686-81ca05103069", 0, "f7d996cd-afe2-4cbe-bf18-68077f66d4ed", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAELvIUOQN7QK8CYkF3lGw3Fgs8pv2Uhzh7Dmecj0520UBfX+8tWWLc4zLB3OrnZxhPw==", null, false, "5337963a-aa66-4e29-837d-6485a75f35fa", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "698ec717-470c-43d9-8152-6ceb66d87d0d", "a64cea32-50dc-48ae-961a-2b4c46c195f2" },
                    { "4b7fc3d0-e918-4996-9621-e29575a2c653", "f4c1fcbe-0de2-479a-b686-81ca05103069" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "698ec717-470c-43d9-8152-6ceb66d87d0d", "a64cea32-50dc-48ae-961a-2b4c46c195f2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b7fc3d0-e918-4996-9621-e29575a2c653", "f4c1fcbe-0de2-479a-b686-81ca05103069" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b7fc3d0-e918-4996-9621-e29575a2c653");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "698ec717-470c-43d9-8152-6ceb66d87d0d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a64cea32-50dc-48ae-961a-2b4c46c195f2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f4c1fcbe-0de2-479a-b686-81ca05103069");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dc2da46-1f7f-4896-85e7-adda07a196fd", "2", "User", "USER" },
                    { "28baaeb0-6499-451d-bbb8-f143ebd7ef79", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "194b47e7-c27b-47ae-801b-61521ddfbee0", 0, "6f7d31ed-ef66-4a0d-91a8-6f126a169c78", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAENK6PmOQ66yLMFScpjPnuyuGVOXO2SWCW4COkzQ5lG/5/qBllkxtFe2uSlmWzR6cFQ==", null, false, "12bf057b-8327-4ba7-9f7d-d92ed5799ee7", false, "admin@admin.com" },
                    { "45b06f22-a224-449f-9f30-b5224b330f6c", 0, "3b0aaeee-0a16-4101-bac5-f0f76614eae2", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEBWmFZzbynDpPejqI+hXEXpTbnEwRVqhoTUGvwOoPJRVUNW70qp994g8uVomA3oDtg==", null, false, "b986d5bb-40fa-456d-b937-51eaae783b98", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "28baaeb0-6499-451d-bbb8-f143ebd7ef79", "194b47e7-c27b-47ae-801b-61521ddfbee0" },
                    { "0dc2da46-1f7f-4896-85e7-adda07a196fd", "45b06f22-a224-449f-9f30-b5224b330f6c" }
                });
        }
    }
}
