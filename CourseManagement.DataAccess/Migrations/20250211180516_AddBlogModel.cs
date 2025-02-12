using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11ea7f26-3a0b-4ee8-88ec-0bee65ad975d", "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "394e5930-426c-440d-a9f1-fc3a6c59ccf6", "e404e498-c930-4a45-9b22-c35d2f333d37" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11ea7f26-3a0b-4ee8-88ec-0bee65ad975d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "394e5930-426c-440d-a9f1-fc3a6c59ccf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => new { x.BlogId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BlogCategories_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Blogs");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11ea7f26-3a0b-4ee8-88ec-0bee65ad975d", "1", "Admin", "ADMIN" },
                    { "394e5930-426c-440d-a9f1-fc3a6c59ccf6", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2", 0, "2d3d1709-49f6-460a-8ff7-c773ba476be9", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEAPzeY/PW6QDF9Sx3MW2GCdxokoPRCJm77fXCgGP7k0faqo3DFxokeHUKTEE1YNnTQ==", null, false, "6001841f-839f-400f-88f0-4a376a06c267", false, "admin@admin.com" },
                    { "e404e498-c930-4a45-9b22-c35d2f333d37", 0, "ddf63c03-2426-4141-9928-293a8f72f26f", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEH00KkVzE+rnlLbneqdAg/JF5X8aReCva2CzWVIweeBHXfphi7N+dD0bOnpcUGvuhg==", null, false, "d34f3d80-8856-43cd-a298-3e00318f2ab3", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "11ea7f26-3a0b-4ee8-88ec-0bee65ad975d", "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2" },
                    { "394e5930-426c-440d-a9f1-fc3a6c59ccf6", "e404e498-c930-4a45-9b22-c35d2f333d37" }
                });
        }
    }
}
