using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses");

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

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Courses");

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
    }
}
