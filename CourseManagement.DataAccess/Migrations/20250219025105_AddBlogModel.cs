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
                name: "BlogCategory",
                columns: table => new
                {
                    BlogsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => new { x.BlogsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BlogCategory_Blogs_BlogsId",
                        column: x => x.BlogsId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "872d8b9e-f131-4b05-b414-9877ead63ec2", "1", "Admin", "ADMIN" },
                    { "c5ca0b18-66be-4784-b545-1673abae0928", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "19b04c72-0c3f-4cc6-93b9-87b463bd50ed", 0, "3b9d6f7a-6777-4f16-8db9-35193c07ef84", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAED4AxGWL7I4W91nv5Ud9BjuU74pkO5JqOuO/vK4dMkZmS6ur+0yaMn22PFTebuXJCg==", null, false, "36f04fd5-20fa-4204-80b1-890750b2c3e1", false, "admin@admin.com" },
                    { "8b5ab08f-0fb6-471e-adbf-1d492257e658", 0, "2645b3d5-0bf1-4aa4-8462-f314faed0841", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEIN+g0NL6lu0IfKAqH5TSc55e28IdX68DJSG+pXBwoPOIVO2A0cMpCY9hN2xtjiiWA==", null, false, "59f10ee5-9fe1-44a6-ae91-08926a5a0850", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "872d8b9e-f131-4b05-b414-9877ead63ec2", "19b04c72-0c3f-4cc6-93b9-87b463bd50ed" },
                    { "c5ca0b18-66be-4784-b545-1673abae0928", "8b5ab08f-0fb6-471e-adbf-1d492257e658" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_CategoriesId",
                table: "BlogCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "872d8b9e-f131-4b05-b414-9877ead63ec2", "19b04c72-0c3f-4cc6-93b9-87b463bd50ed" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c5ca0b18-66be-4784-b545-1673abae0928", "8b5ab08f-0fb6-471e-adbf-1d492257e658" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "872d8b9e-f131-4b05-b414-9877ead63ec2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5ca0b18-66be-4784-b545-1673abae0928");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19b04c72-0c3f-4cc6-93b9-87b463bd50ed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b5ab08f-0fb6-471e-adbf-1d492257e658");

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
