using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoDurationToLessonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "VideoDuration",
                table: "Lessons",
                type: "time",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66048fe0-84cd-4135-a8e0-1f66aa02b59a", "AQAAAAIAAYagAAAAEDrhg++yJqx23XBjozBRRKx/GGD2/hEUpethkHNfDlyvHgyt4IFcrrI1EKoGfGYQVw==", "3a1e4fe5-8d2d-4cb8-b43d-6384dd586e24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e404e498-c930-4a45-9b22-c35d2f333d37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e60150e-173c-4ed7-b4db-aa6d893b2eb9", "AQAAAAIAAYagAAAAEINP+cXpb/aseeykMmv7m7TREmJKPWTvJWTZTMXIecrzjzgFwtJ4IM/TM3o8Q5mcGQ==", "10aa26ad-a2e6-4d2b-aeca-464ac2da53f1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoDuration",
                table: "Lessons");

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
