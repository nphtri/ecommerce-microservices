using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRODUCTSERVICE.API.Migrations
{
  public partial class addbanners : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "banners",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            description = table.Column<string>(type: "nvarchar(200)", nullable: true),
            image = table.Column<string>(type: "varchar(200)", nullable: false),
            is_active = table.Column<bool>(type: "bit", nullable: false),
            order_index = table.Column<int>(type: "int", nullable: false),
            target_url = table.Column<string>(type: "varchar(200)", nullable: true),
            created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_banners", x => x.id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "banners");
    }
  }
}
