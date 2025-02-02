using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixInitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e7cde99f-b48f-46c9-9614-00173d1e4097", "a55ebcc4-f99c-4952-aa76-c50a294fe195" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ed4fedf-599e-4472-9d34-ff2169fd9b3d", "c8552719-4914-4198-b6ba-6aa8f7abc9ac" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed4fedf-599e-4472-9d34-ff2169fd9b3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7cde99f-b48f-46c9-9614-00173d1e4097");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a55ebcc4-f99c-4952-aa76-c50a294fe195");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8552719-4914-4198-b6ba-6aa8f7abc9ac");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LessonId",
                table: "Comments",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Lessons_LessonId",
                table: "Comments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Lessons_LessonId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_LessonId",
                table: "Comments");

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

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ed4fedf-599e-4472-9d34-ff2169fd9b3d", "2", "User", "USER" },
                    { "e7cde99f-b48f-46c9-9614-00173d1e4097", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a55ebcc4-f99c-4952-aa76-c50a294fe195", 0, "fbcf926d-74a9-4cb8-a6a0-b507189b8ccd", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEEPLHgbk143fw6OglbBfQxBeXsGQbORI6vLN4Dto9vqpV7Ex+XsEXehOapnujMHBPg==", null, false, "1b6550d7-e2a6-4ce4-893b-1cb022c4f0f9", false, "admin@admin.com" },
                    { "c8552719-4914-4198-b6ba-6aa8f7abc9ac", 0, "a35db251-1d49-4491-b7d1-795753919f88", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEKgzh4j4d/EM2iKaTzHjxQItcM4xAp7as1hQtbJ+gYBHXkD5uv1sWZ1dMva7gdvqEg==", null, false, "3dfd1274-3a24-4457-af82-61ed9df62a37", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e7cde99f-b48f-46c9-9614-00173d1e4097", "a55ebcc4-f99c-4952-aa76-c50a294fe195" },
                    { "5ed4fedf-599e-4472-9d34-ff2169fd9b3d", "c8552719-4914-4198-b6ba-6aa8f7abc9ac" }
                });
        }
    }
}
