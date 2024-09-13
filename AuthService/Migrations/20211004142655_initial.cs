using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(200)", nullable: false),
                    hashed = table.Column<string>(type: "varchar(150)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", nullable: false),
                    failed_accessed_times = table.Column<int>(type: "int", nullable: false),
                    is_email_verified = table.Column<bool>(type: "bit", nullable: false),
                    is_phone_verified = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    last_accessed = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_accounts_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_role_id",
                table: "accounts",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
