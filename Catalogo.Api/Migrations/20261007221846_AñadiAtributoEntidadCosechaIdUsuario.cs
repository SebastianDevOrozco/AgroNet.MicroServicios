using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Api.Migrations
{
    /// <inheritdoc />
    public partial class AñadiAtributoEntidadCosechaIdUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Cosechas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Cosechas");
        }
    }
}
