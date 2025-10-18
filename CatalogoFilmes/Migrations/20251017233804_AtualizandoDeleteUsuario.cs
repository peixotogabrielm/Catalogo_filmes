using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoFilmes.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoDeleteUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Usuarios_CriadoPorId",
                table: "Filmes");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Usuarios_CriadoPorId",
                table: "Filmes",
                column: "CriadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Usuarios_CriadoPorId",
                table: "Filmes");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Usuarios_CriadoPorId",
                table: "Filmes",
                column: "CriadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
