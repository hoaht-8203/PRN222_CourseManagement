using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountVipAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VipExpirationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VipPrice",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VipStatus",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasedPlan = table.Column<int>(type: "int", nullable: false),
                    VipPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VipExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "VipExpirationDate", "VipPrice", "VipStatus" },
                values: new object[] { "bf29052a-02a1-4094-858b-1fe3d790b06c", "AQAAAAIAAYagAAAAEJf3XGD3XGJPNOy82YN8eUR8u1OnnFele0nln0mTKLiIVmyBe0Buj12mvNvY+otMbg==", "5d3305bf-8c6d-4ddc-a4f6-fa84583f997a", null, null, 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "VipExpirationDate", "VipPrice", "VipStatus" },
                values: new object[] { "976aa71b-dbb2-4d1d-b91d-b4e29742023c", "AQAAAAIAAYagAAAAENZ23dBqvU7tlL6XF29YyCmRTMHWZTjPzwggt838hxMRY6kVxjiDNjbOv/r16CaTkw==", "c90be961-a433-4c5e-b63b-4cbcb917c502", null, null, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "VipExpirationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VipPrice",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VipStatus",
                table: "AspNetUsers");

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
        }
    }
}
