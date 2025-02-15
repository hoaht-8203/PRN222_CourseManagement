using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonProgressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Lessons");

            migrationBuilder.CreateTable(
                name: "LessonProgress",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonProgress", x => new { x.UserId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_LessonProgress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonProgress_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgress_LessonId",
                table: "LessonProgress",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonProgress");

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

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
