using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "635375f0-6395-4c59-b31e-3ae026f855c3", "AQAAAAIAAYagAAAAEKs+mDAleHj0nNqbni8g+A3jKxTOsDWcjiZ9cQPzYZR9jNf73wz65VPZHrQO9rBAJg==", "01623f11-e2cf-4f6d-abe6-b3f7efec7d5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56f2b3e9-17ea-48a4-bc60-2213a6723d08", "AQAAAAIAAYagAAAAEGo5l6MmBdEw8EShx5nzxdfERbEDgoiGtJxjnNJsGstrXJazCnKccVNyHfMmjJXsTg==", "4e7f0ef4-3626-47f6-b751-aab4655e1e84" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LessonId",
                table: "Notes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60676bc7-d464-408f-bce0-36e26ffe577d", "AQAAAAIAAYagAAAAEH4EKJzyhCslni9SU5Yvv/eDvClAOa+YWXDtH+THdXbNKwrOHS5rF94Y4rzlIgofzQ==", "c384c5eb-55f5-47f5-913e-84eff422256a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35b206a2-209f-4d2d-8734-a008bd6910d1", "AQAAAAIAAYagAAAAEDExjTohNg6x+Pa+TieL+hLxObJvdhiFXa72e7zFchXLMNWsZWhOFUEv/oSrE6Yk1Q==", "71afd12e-d149-4b7a-bf3b-b6b0e633fe81" });
        }
    }
}
