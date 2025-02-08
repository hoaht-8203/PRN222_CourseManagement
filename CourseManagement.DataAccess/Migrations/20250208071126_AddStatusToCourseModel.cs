using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToCourseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "885a9ef1-6867-4eed-96ea-e8d44c4a8083", "bf39c843-6bec-4d08-ae35-5fa5ad90ba5f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f61d7250-8526-449e-b601-f0b744178284", "f048ac45-c890-4e0e-bc9a-f619e4fc1323" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "885a9ef1-6867-4eed-96ea-e8d44c4a8083");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f61d7250-8526-449e-b601-f0b744178284");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bf39c843-6bec-4d08-ae35-5fa5ad90ba5f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f048ac45-c890-4e0e-bc9a-f619e4fc1323");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "885a9ef1-6867-4eed-96ea-e8d44c4a8083", "2", "User", "USER" },
                    { "f61d7250-8526-449e-b601-f0b744178284", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "bf39c843-6bec-4d08-ae35-5fa5ad90ba5f", 0, "8c8f9b67-91e8-40f4-bf4f-f16827b828e2", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEGAsLhjagAj32w6CTGsFSySt2RLZx+8re/n3tm2Z0rYrtdQSO+oOuZomh24gSXB2nA==", null, false, "bc61610e-3080-4b8f-908e-b14ea34a1a79", false, "user@user.com" },
                    { "f048ac45-c890-4e0e-bc9a-f619e4fc1323", 0, "def9f211-d384-450c-80f6-714ed86354b9", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEKnDneNtomWnz4BKBFgELyqlclGOzFHmeVCl4lanERjTMH6dvb6xsVSYQBzWM9oaRg==", null, false, "060ea66d-5377-469e-a209-b081a6034a61", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "885a9ef1-6867-4eed-96ea-e8d44c4a8083", "bf39c843-6bec-4d08-ae35-5fa5ad90ba5f" },
                    { "f61d7250-8526-449e-b601-f0b744178284", "f048ac45-c890-4e0e-bc9a-f619e4fc1323" }
                });
        }
    }
}
