using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPrevAndNextLessonIdToLessonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9d959454-9c14-4236-b90b-42f4b3452bf2", "58b27de1-21e7-4218-89cb-de4e742ec38f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "8380a4a8-c998-4c75-91d7-164ecc392220" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bf45bea-1e92-4d7a-a56b-5af5c1352931");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d959454-9c14-4236-b90b-42f4b3452bf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58b27de1-21e7-4218-89cb-de4e742ec38f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8380a4a8-c998-4c75-91d7-164ecc392220");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61f7d1e4-0dbb-4252-8afa-c66f17db0c32", "1", "Admin", "ADMIN" },
                    { "fe211d34-7a94-4118-814f-4d210ab64eae", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "af3f41e2-bc3c-49db-9dc9-b687723eeaeb", 0, "6ae24fa4-0714-4197-9da2-9f87d3d67f81", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEHDVjPK6Ez4m2QRv3GZjF/bLZBhosb59wxLoUtt1g2pSf1Adwy3MR3o8P1LJDoqmzg==", null, false, "b8970f9b-312c-4731-84ef-c138ff33b9d2", false, "admin@admin.com" },
                    { "c1edeb67-43e8-4e89-8231-bfb29a061f2e", 0, "9e4df34b-dafe-488c-86f3-6626c50100e7", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEO8thcKX6Cb4j/TvWQAqKAqyvmXPAvCezgl0J4/BxCk24/YKJIM6zsPM6JXNGSuW7Q==", null, false, "aad90be3-e558-425a-8e96-3eda1d8629d2", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "61f7d1e4-0dbb-4252-8afa-c66f17db0c32", "af3f41e2-bc3c-49db-9dc9-b687723eeaeb" },
                    { "fe211d34-7a94-4118-814f-4d210ab64eae", "c1edeb67-43e8-4e89-8231-bfb29a061f2e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "61f7d1e4-0dbb-4252-8afa-c66f17db0c32", "af3f41e2-bc3c-49db-9dc9-b687723eeaeb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fe211d34-7a94-4118-814f-4d210ab64eae", "c1edeb67-43e8-4e89-8231-bfb29a061f2e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61f7d1e4-0dbb-4252-8afa-c66f17db0c32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe211d34-7a94-4118-814f-4d210ab64eae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af3f41e2-bc3c-49db-9dc9-b687723eeaeb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1edeb67-43e8-4e89-8231-bfb29a061f2e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "1", "Admin", "ADMIN" },
                    { "9d959454-9c14-4236-b90b-42f4b3452bf2", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "58b27de1-21e7-4218-89cb-de4e742ec38f", 0, "fce6649b-3ec6-4780-874b-9272b3a9656c", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAENyUemfAFNT8YxOwBfjSiR55ckOFvZxDxgRQozI347Edj0e5APPZ1krmNA1R8AZgDQ==", null, false, "02ecd107-efe7-4b73-a0b2-29b40e3659c2", false, "user@user.com" },
                    { "8380a4a8-c998-4c75-91d7-164ecc392220", 0, "5065ce28-b45b-41e0-80f2-10466a3b6602", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEB0i+LZHdx+yckMzdmpyH9q/FcQte4fwcPsvYpgyiHGRuH5fp9pZOTJnbcykUefZfA==", null, false, "1dd2bd12-6fb6-4449-be6c-fc5133ccf9b9", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9d959454-9c14-4236-b90b-42f4b3452bf2", "58b27de1-21e7-4218-89cb-de4e742ec38f" },
                    { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "8380a4a8-c998-4c75-91d7-164ecc392220" }
                });
        }
    }
}
