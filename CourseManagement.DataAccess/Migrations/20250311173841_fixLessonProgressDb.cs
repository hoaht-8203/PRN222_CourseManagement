using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixLessonProgressDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_AspNetUsers_UserId",
                table: "LessonProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_Lessons_LessonId",
                table: "LessonProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonProgress",
                table: "LessonProgress");

            migrationBuilder.RenameTable(
                name: "LessonProgress",
                newName: "LessonProgresses");

            migrationBuilder.RenameIndex(
                name: "IX_LessonProgress_LessonId",
                table: "LessonProgresses",
                newName: "IX_LessonProgresses_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonProgresses",
                table: "LessonProgresses",
                columns: new[] { "UserId", "LessonId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId",
                table: "LessonProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_Lessons_LessonId",
                table: "LessonProgresses",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId",
                table: "LessonProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_Lessons_LessonId",
                table: "LessonProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonProgresses",
                table: "LessonProgresses");

            migrationBuilder.RenameTable(
                name: "LessonProgresses",
                newName: "LessonProgress");

            migrationBuilder.RenameIndex(
                name: "IX_LessonProgresses_LessonId",
                table: "LessonProgress",
                newName: "IX_LessonProgress_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonProgress",
                table: "LessonProgress",
                columns: new[] { "UserId", "LessonId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "111602a8-3c6e-4994-bfb7-a305c90acd7c", "AQAAAAIAAYagAAAAEJAsHtVxRbtPi7SRJXD/W3FDT+PUYvmZiS9VdO6ad4gB3WXSf3eiFIDTz+NigTtQ5Q==", "f82ccc12-2511-4947-9ada-551d5af8c766" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "226ac58c-fbe3-4ae3-ac02-88f0e5df8fc5", "AQAAAAIAAYagAAAAEJc72OsuCPe7CJyn4PA3Fsr30YBRy5EV8gl77P/e5wrZq6Beu0mCldTN/zEOZC4Kig==", "6fe14a42-a3d9-4484-81d8-daec8e94c185" });

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_AspNetUsers_UserId",
                table: "LessonProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_Lessons_LessonId",
                table: "LessonProgress",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
