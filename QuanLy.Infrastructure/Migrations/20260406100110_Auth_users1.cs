using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Auth_users1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Auth_Users",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Auth_Users",
                newName: "ID");
        }
    }
}
