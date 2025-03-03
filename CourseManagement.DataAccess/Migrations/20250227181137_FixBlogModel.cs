using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixBlogModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caf499c3-9c43-4a31-b9f4-9438c80b9a8a", "AQAAAAIAAYagAAAAEPgtnDCQRrt6l55lDjWwdjb6DlUZr4lXxkwZY2grSWkLsoZ6q1wmYArbhAs6075LMQ==", "c2422391-d178-4470-a9fc-6991d5838eff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "624a3dd5-0bd4-421f-9ee0-aca7a487f156", "AQAAAAIAAYagAAAAEChWyiPKBf/6+e+hrj4OcCvyM4zZaqhVIdflFB36hoA598I9MSyH+TLPdZcn2NpOdw==", "df335363-4654-4178-ab15-0785d4f495c1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd99cec4-001a-4278-8df7-50c3175a0350", "AQAAAAIAAYagAAAAEBxdbrGWrkIXnv6eKSuQE6YB2q368aHqSQ0M8N2SFG0lzTudGWq1GKMge1gf0f0hmw==", "768ad81f-d2c1-4c2a-be43-d783e455d400" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d040fc6-650d-472a-848a-75b6f4386f61", "AQAAAAIAAYagAAAAEPqfNnQvvSYNlRYq4YF2eqob9sO/FYSQePR62JdLQHdlMEW7exwvcrevNx8Vr/BtHA==", "68ba4162-89cb-41fa-87bd-66d2d9bcaf75" });
        }
    }
}
