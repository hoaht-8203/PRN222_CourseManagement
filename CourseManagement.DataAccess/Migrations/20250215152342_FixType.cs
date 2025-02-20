using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "069953bc-7cdb-40c8-8f40-0bcc4ec17a17" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "56a4b486-f53c-4799-9302-e8df2aea3654" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49d7fa12-4b73-4e02-b7fa-b0750b4a7389");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cb1723f-5346-4984-b915-a2a6ed6efe53");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "069953bc-7cdb-40c8-8f40-0bcc4ec17a17");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56a4b486-f53c-4799-9302-e8df2aea3654");

            migrationBuilder.RenameColumn(
                name: "preModuleId",
                table: "Modules",
                newName: "PreModuleId");

            migrationBuilder.RenameColumn(
                name: "nextModuleId",
                table: "Modules",
                newName: "NextModuleId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "1", "Admin", "ADMIN" },
                    { "9d959454-9c14-4236-b90b-42f4b3452bf2", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "58b27de1-21e7-4218-89cb-de4e742ec38f", 0, "fce6649b-3ec6-4780-874b-9272b3a9656c", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAENyUemfAFNT8YxOwBfjSiR55ckOFvZxDxgRQozI347Edj0e5APPZ1krmNA1R8AZgDQ==", null, false, "02ecd107-efe7-4b73-a0b2-29b40e3659c2", false, "user@user.com" },
                    { "8380a4a8-c998-4c75-91d7-164ecc392220", 0, "5065ce28-b45b-41e0-80f2-10466a3b6602", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEB0i+LZHdx+yckMzdmpyH9q/FcQte4fwcPsvYpgyiHGRuH5fp9pZOTJnbcykUefZfA==", null, false, "1dd2bd12-6fb6-4449-be6c-fc5133ccf9b9", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9d959454-9c14-4236-b90b-42f4b3452bf2", "58b27de1-21e7-4218-89cb-de4e742ec38f" },
                    { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "8380a4a8-c998-4c75-91d7-164ecc392220" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9d959454-9c14-4236-b90b-42f4b3452bf2", "58b27de1-21e7-4218-89cb-de4e742ec38f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3bf45bea-1e92-4d7a-a56b-5af5c1352931", "8380a4a8-c998-4c75-91d7-164ecc392220" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bf45bea-1e92-4d7a-a56b-5af5c1352931");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d959454-9c14-4236-b90b-42f4b3452bf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58b27de1-21e7-4218-89cb-de4e742ec38f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8380a4a8-c998-4c75-91d7-164ecc392220");

            migrationBuilder.RenameColumn(
                name: "PreModuleId",
                table: "Modules",
                newName: "preModuleId");

            migrationBuilder.RenameColumn(
                name: "NextModuleId",
                table: "Modules",
                newName: "nextModuleId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "2", "User", "USER" },
                    { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "069953bc-7cdb-40c8-8f40-0bcc4ec17a17", 0, "f685aedd-998d-440a-ae17-6d6aba91d9a5", "user@user.com", true, "User", true, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEJE3CFRxUwwbbSd4xw/dWW6vWTCxPROOf1DkxN43V6H2YGB5bjt8rnuHbbhmi/2bUA==", null, false, "77745fae-3e47-44f7-bc6d-6c38c28c9dce", false, "user@user.com" },
                    { "56a4b486-f53c-4799-9302-e8df2aea3654", 0, "8c84eaf0-3a61-4b05-a67b-3d2afcf7eca4", "admin@admin.com", true, "Admin", true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEG3UDDF06e+rBmMwbz2jgIXAkBlvaLKDFxhBKuSphagMnVa67XbLMySTh+/00ywh0A==", null, false, "5de5517d-bdc6-4c3d-abcd-6d80a563b51a", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "49d7fa12-4b73-4e02-b7fa-b0750b4a7389", "069953bc-7cdb-40c8-8f40-0bcc4ec17a17" },
                    { "9cb1723f-5346-4984-b915-a2a6ed6efe53", "56a4b486-f53c-4799-9302-e8df2aea3654" }
                });
        }
    }
}
