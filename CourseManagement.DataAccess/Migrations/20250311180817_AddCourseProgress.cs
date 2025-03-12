using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastViewedLessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProgresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseProgresses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProgresses_Lessons_LastViewedLessonId",
                        column: x => x.LastViewedLessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgresses_CourseId",
                table: "CourseProgresses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgresses_LastViewedLessonId",
                table: "CourseProgresses",
                column: "LastViewedLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgresses_UserId_CourseId",
                table: "CourseProgresses",
                columns: new[] { "UserId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseProgresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ce23955-fe70-4fd2-a34d-2a2e91046f64", "AQAAAAIAAYagAAAAENf9pfDYiY4tPtNBb1Vr6zGJGUePVvxWotJ53n5uFjlYgJJcHQe3yxrPO9gCjjNVqw==", "7398f9a7-edd9-4921-91e7-27b0595a6458" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d86bad58-fb25-43a8-b200-3c7c8962b992", "AQAAAAIAAYagAAAAEKJfT5turqx8CcCMe0fadXs3CjJFv7QbBcKU4HkgFBsC2Lz4ODblUO7p5n/vcWfkvw==", "7da03c19-e4a3-4d24-bcc9-65ed9abc596c" });
        }
    }
}
