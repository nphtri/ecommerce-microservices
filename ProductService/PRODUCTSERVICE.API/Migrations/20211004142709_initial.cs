using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRODUCTSERVICE.API.Migrations
{
  public partial class initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "art_collections",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            name = table.Column<string>(type: "nvarchar(200)", nullable: false),
            publisher = table.Column<string>(type: "nvarchar(200)", nullable: false),
            published_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            image = table.Column<string>(type: "varchar(200)", nullable: false),
            description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
            created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_art_collections", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "artists",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            first_name = table.Column<string>(type: "nvarchar(200)", nullable: false),
            last_name = table.Column<string>(type: "nvarchar(200)", nullable: false),
            nickname = table.Column<string>(type: "nvarchar(200)", nullable: true),
            avatar = table.Column<string>(type: "varchar(200)", nullable: false),
            biography = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
            phone = table.Column<string>(type: "varchar(15)", nullable: false),
            email = table.Column<string>(type: "varchar(30)", nullable: false),
            instagram = table.Column<string>(type: "varchar(50)", nullable: true),
            facebook = table.Column<string>(type: "varchar(50)", nullable: true),
            created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_artists", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "lookup_types",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            value = table.Column<string>(type: "nvarchar(200)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_lookup_types", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "lookups",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            value = table.Column<string>(type: "nvarchar(200)", nullable: false),
            lookup_type_id = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_lookups", x => x.id);
            table.ForeignKey(
                      name: "FK_lookups_lookup_types_lookup_type_id",
                      column: x => x.lookup_type_id,
                      principalTable: "lookup_types",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "arts",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            name = table.Column<string>(type: "nvarchar(200)", nullable: false),
            artist_id = table.Column<int>(type: "int", nullable: false),
            style_id = table.Column<int>(type: "int", nullable: false),
            description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
            @short = table.Column<string>(name: "short", type: "nvarchar(500)", nullable: true),
            image = table.Column<string>(type: "varchar(200)", nullable: false),
            collection_id = table.Column<int>(type: "int", nullable: true),
            created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_arts", x => x.id);
            table.ForeignKey(
                      name: "FK_arts_art_collections_collection_id",
                      column: x => x.collection_id,
                      principalTable: "art_collections",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_arts_artists_artist_id",
                      column: x => x.artist_id,
                      principalTable: "artists",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_arts_lookups_style_id",
                      column: x => x.style_id,
                      principalTable: "lookups",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "products",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            art_id = table.Column<int>(type: "int", nullable: true),
            name = table.Column<string>(type: "nvarchar(500)", nullable: false),
            description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
            type_id = table.Column<int>(type: "int", nullable: false),
            price = table.Column<double>(type: "float", nullable: false),
            image = table.Column<string>(type: "varchar(200)", nullable: false),
            created_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
            modified_time = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_products", x => x.id);
            table.ForeignKey(
                      name: "FK_products_arts_art_id",
                      column: x => x.art_id,
                      principalTable: "arts",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_products_lookups_type_id",
                      column: x => x.type_id,
                      principalTable: "lookups",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_arts_artist_id",
          table: "arts",
          column: "artist_id");

      migrationBuilder.CreateIndex(
          name: "IX_arts_collection_id",
          table: "arts",
          column: "collection_id");

      migrationBuilder.CreateIndex(
          name: "IX_arts_style_id",
          table: "arts",
          column: "style_id");

      migrationBuilder.CreateIndex(
          name: "IX_lookups_lookup_type_id",
          table: "lookups",
          column: "lookup_type_id");

      migrationBuilder.CreateIndex(
          name: "IX_products_art_id",
          table: "products",
          column: "art_id");

      migrationBuilder.CreateIndex(
          name: "IX_products_type_id",
          table: "products",
          column: "type_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "products");

      migrationBuilder.DropTable(
          name: "arts");

      migrationBuilder.DropTable(
          name: "art_collections");

      migrationBuilder.DropTable(
          name: "artists");

      migrationBuilder.DropTable(
          name: "lookups");

      migrationBuilder.DropTable(
          name: "lookup_types");
    }
  }
}
