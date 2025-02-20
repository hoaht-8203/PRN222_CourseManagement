using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPreAndNextModuleIdToModuleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "nextModuleId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "preModuleId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "2", "User", "USER" },
                    { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "069953bc-7cdb-40c8-8f40-0bcc4ec17a17", 0, "f685aedd-998d-440a-ae17-6d6aba91d9a5", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEJE3CFRxUwwbbSd4xw/dWW6vWTCxPROOf1DkxN43V6H2YGB5bjt8rnuHbbhmi/2bUA==", null, false, "77745fae-3e47-44f7-bc6d-6c38c28c9dce", false, "user@user.com" },
                    { "56a4b486-f53c-4799-9302-e8df2aea3654", 0, "8c84eaf0-3a61-4b05-a67b-3d2afcf7eca4", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEG3UDDF06e+rBmMwbz2jgIXAkBlvaLKDFxhBKuSphagMnVa67XbLMySTh+/00ywh0A==", null, false, "5de5517d-bdc6-4c3d-abcd-6d80a563b51a", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "069953bc-7cdb-40c8-8f40-0bcc4ec17a17" },
                    { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "56a4b486-f53c-4799-9302-e8df2aea3654" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "069953bc-7cdb-40c8-8f40-0bcc4ec17a17" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "56a4b486-f53c-4799-9302-e8df2aea3654" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49d7fa12-4b73-4e02-b7fa-b0750b4a7389");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cb1723f-5346-4984-b915-a2a6ed6efe53");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "069953bc-7cdb-40c8-8f40-0bcc4ec17a17");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56a4b486-f53c-4799-9302-e8df2aea3654");

            migrationBuilder.DropColumn(
                name: "nextModuleId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "preModuleId",
                table: "Modules");

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
    }
}
