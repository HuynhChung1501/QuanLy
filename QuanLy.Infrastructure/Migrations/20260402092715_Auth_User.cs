using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Auth_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth_Assign",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "text", nullable: false),
                    ObjectID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Assign", x => x.Permission);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Assign_role",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "text", nullable: false),
                    ObjectID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Assign_role", x => x.Permission);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Permissions",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Permissions", x => x.Permission);
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserRoles",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserRoles", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    UsereName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    FullName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PassWord = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    IsShow = table.Column<byte>(type: "smallint", nullable: false),
                    Gender = table.Column<byte>(type: "smallint", nullable: false),
                    Active = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Desc = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsSysAdmin = table.Column<byte>(type: "smallint", nullable: false),
                    IsShow = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.RoleID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_Assign");

            migrationBuilder.DropTable(
                name: "Auth_Assign_role");

            migrationBuilder.DropTable(
                name: "Auth_Permissions");

            migrationBuilder.DropTable(
                name: "Auth_UserRoles");

            migrationBuilder.DropTable(
                name: "Auth_Users");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
