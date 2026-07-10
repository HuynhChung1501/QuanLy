using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Auth_Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auth_Roles",
                table: "Auth_Roles",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Auth_Roles",
                table: "Auth_Roles");

            migrationBuilder.RenameTable(
                name: "Auth_Roles",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "RoleID");
        }
    }
}
