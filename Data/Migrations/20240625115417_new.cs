using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Flowers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_CategoryId",
                table: "Flowers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Categories_CategoryId",
                table: "Flowers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Categories_CategoryId",
                table: "Flowers");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_CategoryId",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Flowers");

            migrationBuilder.CreateTable(
                name: "FlowerCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FlowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerCategory_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerCategory_CategoryId",
                table: "FlowerCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerCategory_FlowerId",
                table: "FlowerCategory",
                column: "FlowerId");
        }
    }
}
