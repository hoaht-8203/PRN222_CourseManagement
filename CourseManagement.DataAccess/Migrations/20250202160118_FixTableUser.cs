using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a7cb3e3-4428-4b51-8ceb-7247480e0561", "2", "User", "USER" },
                    { "7aa07345-c766-4087-b463-e60f7733ac5f", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3774afbf-bac1-4aa9-ad31-bf530e2f7256", 0, "acfa08ac-8601-4367-8f89-801f2912360d", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEMXcxtG6NsN84qEAUuIu5egPVW84tzBWQve8WMR4q1wwJu1MBb1IjIHO4z3tuzFrHA==", null, false, "d16fa597-e024-4fbe-84a0-94f0885147fd", false, "admin@admin.com" },
                    { "d63e56bb-1658-4ea3-b645-ad7ced4490fc", 0, "8ffe9a87-9842-42b2-8f5b-815050986b13", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEGRUnuAXi3QEvodUuj7dXRkIYKMVuymEEGsl887DlpkhO30Th6sp0ESajaefq5zmvw==", null, false, "b52080dc-87f9-42ac-8380-495bea628d76", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7aa07345-c766-4087-b463-e60f7733ac5f", "3774afbf-bac1-4aa9-ad31-bf530e2f7256" },
                    { "6a7cb3e3-4428-4b51-8ceb-7247480e0561", "d63e56bb-1658-4ea3-b645-ad7ced4490fc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7aa07345-c766-4087-b463-e60f7733ac5f", "3774afbf-bac1-4aa9-ad31-bf530e2f7256" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6a7cb3e3-4428-4b51-8ceb-7247480e0561", "d63e56bb-1658-4ea3-b645-ad7ced4490fc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a7cb3e3-4428-4b51-8ceb-7247480e0561");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7aa07345-c766-4087-b463-e60f7733ac5f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3774afbf-bac1-4aa9-ad31-bf530e2f7256");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d63e56bb-1658-4ea3-b645-ad7ced4490fc");

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
    }
}
