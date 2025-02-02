using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixAppUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d20f9ed-b79d-4f04-8c56-cdd8891cbcd8", "b8042fdf-d920-4454-9f98-aea3aaf38a9d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "abfdb9e5-ae85-4538-8e84-0fc95304afb1", "c9357687-3e0e-4c88-bf87-f2ad99128d24" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d20f9ed-b79d-4f04-8c56-cdd8891cbcd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abfdb9e5-ae85-4538-8e84-0fc95304afb1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8042fdf-d920-4454-9f98-aea3aaf38a9d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9357687-3e0e-4c88-bf87-f2ad99128d24");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85d9f1be-9af4-4ad1-add2-d2850198264e", "2", "User", "USER" },
                    { "c8bf6f96-ffcd-4fea-a942-7848f314ab92", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5319c417-b66b-4793-9918-e1decad950f5", 0, "7d87c71f-1ab2-4bfd-868c-3a6fcf00be73", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAECFjDyhY+QJjS29x8t9kORyxw/DV1ds3t58u1mBX2ISPXJs+X0sGSnP4SFDCu779Vw==", null, false, "8bbd98b2-a03e-4cd8-ac5b-5c077889bb0a", false, "user@user.com" },
                    { "c2d15a37-46ee-4a76-919b-9cad3babfd0a", 0, "6c1e1c93-90d8-4f32-87b8-ec697c2084cd", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEJMqMLwWVK3jr/0Yu3mL30O8wZhHcnh6am3k3lWIxMobiDR3CFPw7RVAEnjbzRLh8A==", null, false, "bf21d2ce-6c50-4bb3-8dc1-82314816a108", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "85d9f1be-9af4-4ad1-add2-d2850198264e", "5319c417-b66b-4793-9918-e1decad950f5" },
                    { "c8bf6f96-ffcd-4fea-a942-7848f314ab92", "c2d15a37-46ee-4a76-919b-9cad3babfd0a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "85d9f1be-9af4-4ad1-add2-d2850198264e", "5319c417-b66b-4793-9918-e1decad950f5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8bf6f96-ffcd-4fea-a942-7848f314ab92", "c2d15a37-46ee-4a76-919b-9cad3babfd0a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85d9f1be-9af4-4ad1-add2-d2850198264e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8bf6f96-ffcd-4fea-a942-7848f314ab92");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5319c417-b66b-4793-9918-e1decad950f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c2d15a37-46ee-4a76-919b-9cad3babfd0a");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d20f9ed-b79d-4f04-8c56-cdd8891cbcd8", "1", "Admin", "ADMIN" },
                    { "abfdb9e5-ae85-4538-8e84-0fc95304afb1", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "b8042fdf-d920-4454-9f98-aea3aaf38a9d", 0, "77315747-8474-4a48-81c5-b52542277323", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEErgAKA3PHbHvxjSTFrXFiB1cDY07Hmf/Rmp6b2VLD3GUp5QRXC0mTJFnC7okXw+8g==", null, false, "122b586b-08f3-480d-b580-6bce61a35169", false, "admin@admin.com" },
                    { "c9357687-3e0e-4c88-bf87-f2ad99128d24", 0, "4bf0b599-dc13-4745-b9e8-32e35b46d454", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAENfPrR6jA7vA4u5vxZ8ah8W8EjZXS4iO5FlYOX7AOXKBYkcNVcQpBjliklCUvZmOnQ==", null, false, "a476897a-6d62-4803-ae59-2f4b32ba8028", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5d20f9ed-b79d-4f04-8c56-cdd8891cbcd8", "b8042fdf-d920-4454-9f98-aea3aaf38a9d" },
                    { "abfdb9e5-ae85-4538-8e84-0fc95304afb1", "c9357687-3e0e-4c88-bf87-f2ad99128d24" }
                });
        }
    }
}
