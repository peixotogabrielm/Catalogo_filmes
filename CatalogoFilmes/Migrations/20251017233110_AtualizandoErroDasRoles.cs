using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoFilmes.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoErroDasRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Role",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Role",
                table: "Usuarios",
                column: "Role",
                unique: true);
        }
    }
}
