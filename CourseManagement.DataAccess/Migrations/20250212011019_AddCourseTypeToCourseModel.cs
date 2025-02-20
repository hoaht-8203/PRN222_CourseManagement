using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseTypeToCourseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bb2c8336-b590-43d0-8f50-d86c48a25ee5", "23a1d2ca-a7d5-4e4d-a0d4-4412645058fe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1c0cc469-7d9a-4d19-9308-7dd6efb453c0", "67fba672-1703-403a-8e8c-22b2dba4aaf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c0cc469-7d9a-4d19-9308-7dd6efb453c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb2c8336-b590-43d0-8f50-d86c48a25ee5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23a1d2ca-a7d5-4e4d-a0d4-4412645058fe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67fba672-1703-403a-8e8c-22b2dba4aaf6");

            migrationBuilder.AddColumn<int>(
                name: "CourseType",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "afb35093-944c-46cf-9e25-45bc4c0566bf", "2", "User", "USER" },
                    { "ec294c18-285b-4fed-8ce6-bedcf098bda4", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7b78c639-5f0c-450a-9096-bb42a760fe86", 0, "23bd9fd2-29ec-438f-9c96-35236ad9ae0a", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEPvNjP6sTTCir/mL25l4HkHgVMmWfQWdCZ83jhFLDVQqE3BCVAFezpmrL3mhCm/Q+g==", null, false, "c62b94c8-ec9a-4544-863c-ee8df90589f8", false, "user@user.com" },
                    { "f575ca17-e732-412a-a7f0-b0bc616b1155", 0, "a784adc8-fd06-47bf-ae16-7a7d60777e21", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEJSwk8fRJwhDIpjA8hCuSX6ZZIeqNof420XP6pAHBUfwxSE7e/6Q0c3mQft9T+FtRg==", null, false, "53ffd2e1-43c3-41e1-991e-e25d847b2e5e", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "afb35093-944c-46cf-9e25-45bc4c0566bf", "7b78c639-5f0c-450a-9096-bb42a760fe86" },
                    { "ec294c18-285b-4fed-8ce6-bedcf098bda4", "f575ca17-e732-412a-a7f0-b0bc616b1155" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "afb35093-944c-46cf-9e25-45bc4c0566bf", "7b78c639-5f0c-450a-9096-bb42a760fe86" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ec294c18-285b-4fed-8ce6-bedcf098bda4", "f575ca17-e732-412a-a7f0-b0bc616b1155" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb35093-944c-46cf-9e25-45bc4c0566bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec294c18-285b-4fed-8ce6-bedcf098bda4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b78c639-5f0c-450a-9096-bb42a760fe86");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f575ca17-e732-412a-a7f0-b0bc616b1155");

            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c0cc469-7d9a-4d19-9308-7dd6efb453c0", "2", "User", "USER" },
                    { "bb2c8336-b590-43d0-8f50-d86c48a25ee5", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "23a1d2ca-a7d5-4e4d-a0d4-4412645058fe", 0, "813bb97b-75f9-4245-8732-d7512d643030", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEC9PG0vCyvPCnMo2EZK9ASfOlWIBlyX1IXQxRKLC4b3MYEQVCPJ61n7PweujT7WS6g==", null, false, "50d159a1-0ed8-4df6-a1e9-200d47ac61c4", false, "admin@admin.com" },
                    { "67fba672-1703-403a-8e8c-22b2dba4aaf6", 0, "0cec020f-d581-4603-9fc9-df9290820edd", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEHsT1nBXwtLADC9k9Dqq8xvj6tXISUoSYNeaAotNxCgrQ5ed7Vyg0zdFnMPShjBCVA==", null, false, "08fe2f2f-681b-4ff6-bb01-466ec1212ac1", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "bb2c8336-b590-43d0-8f50-d86c48a25ee5", "23a1d2ca-a7d5-4e4d-a0d4-4412645058fe" },
                    { "1c0cc469-7d9a-4d19-9308-7dd6efb453c0", "67fba672-1703-403a-8e8c-22b2dba4aaf6" }
                });
        }
    }
}
