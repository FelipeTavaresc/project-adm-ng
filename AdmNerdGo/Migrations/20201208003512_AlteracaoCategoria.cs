using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmNerdGo.Migrations
{
    public partial class AlteracaoCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaPaiId",
                table: "Categoria",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categoria",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_CategoriaPaiId",
                table: "Categoria",
                column: "CategoriaPaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Categoria_CategoriaPaiId",
                table: "Categoria",
                column: "CategoriaPaiId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Categoria_CategoriaPaiId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_CategoriaPaiId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "CategoriaPaiId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categoria");
        }
    }
}
