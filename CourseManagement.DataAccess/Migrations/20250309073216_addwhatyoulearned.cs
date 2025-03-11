using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addwhatyoulearned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseLearningOutcomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLearningOutcomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLearningOutcomes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CourseLearningOutcomes_CourseId",
                table: "CourseLearningOutcomes",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLearningOutcomes");

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
    }
}
