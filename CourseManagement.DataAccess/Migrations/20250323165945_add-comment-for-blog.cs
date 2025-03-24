using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addcommentforblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Comments",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf29052a-02a1-4094-858b-1fe3d790b06c", "AQAAAAIAAYagAAAAEJf3XGD3XGJPNOy82YN8eUR8u1OnnFele0nln0mTKLiIVmyBe0Buj12mvNvY+otMbg==", "5d3305bf-8c6d-4ddc-a4f6-fa84583f997a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "976aa71b-dbb2-4d1d-b91d-b4e29742023c", "AQAAAAIAAYagAAAAENZ23dBqvU7tlL6XF29YyCmRTMHWZTjPzwggt838hxMRY6kVxjiDNjbOv/r16CaTkw==", "c90be961-a433-4c5e-b63b-4cbcb917c502" });
        }
    }
}
