using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class disablecomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a2dab35-b442-4fce-9f0e-977a36716ca0", "AQAAAAIAAYagAAAAELXQisTm6RTLylnKFIAwX+MG7mwAmnkgIyXfUUVYsbrzqupqClHuAVCFmOcZ1REMGQ==", "72f3d7d0-217e-422f-b2ca-d8b0bad112b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b4348b7-2191-4390-bf2a-d361ed4c6b66", "AQAAAAIAAYagAAAAEMATJX52LLlJduA1lcKlHdIePQIK193yxwt64N5yEVmQU/sou2xuddzXtNVxqnOk1A==", "e76f384f-10d5-4a43-b793-65ff8e8b696e" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44c67e94-1716-430e-9ef9-a75cf29619b8", "AQAAAAIAAYagAAAAECQ6drxF5gnK1H7OWHN94wmvI2yfTmXwvi8xTT/kmjT/veE0UMl0VbCw+dUm/niAXg==", "c1d13a49-4f0a-4096-9056-acbb28725c6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06149b26-9056-46fe-9534-09c89e8324fb", "AQAAAAIAAYagAAAAEKKhmohv6N61XF/zrvkttwMwac1AZDE3EGPMbprWmVLCjfxqL6w3ETD/ZQz67urAqg==", "055eab15-dc02-460d-8baa-5d84e7fcf438" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
