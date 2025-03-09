using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_CourseId",
                table: "enrollments",
                newName: "IX_enrollments_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments",
                columns: new[] { "UserId", "CourseId" });

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
                name: "FK_enrollments_AspNetUsers_UserId",
                table: "enrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_Courses_CourseId",
                table: "enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_AspNetUsers_UserId",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_Courses_CourseId",
                table: "enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments");

            migrationBuilder.RenameTable(
                name: "enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_CourseId",
                table: "Enrollment",
                newName: "IX_Enrollment_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "151347a0-0742-4ebe-9242-18099ffc83dd", "AQAAAAIAAYagAAAAEHmAjohybmRZNmUC9cdg36C6x/OLXKcPGYilp6P5tWh7IAnbL85UgwelnBWizlMYXQ==", "bb69c3e7-a79b-46d9-84d1-6ead1e6c1c94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d957ce30-002f-4920-b3d0-7dcc445ddb6e", "AQAAAAIAAYagAAAAEOD6M8BMxngx6q2vh2z6SV1xFh36jaKBhpH2n/gNPRH5ZWAuQHTkJd0AN+98fJkmcQ==", "24c479e7-3c52-462f-9287-221cdd393119" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
