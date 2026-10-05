using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Api.Migrations
{
    /// <inheritdoc />
    public partial class EstadoCosechaModificado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cosechas_estadoCosechas_IdEstadoCosecha",
                table: "Cosechas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estadoCosechas",
                table: "estadoCosechas");

            migrationBuilder.RenameTable(
                name: "estadoCosechas",
                newName: "EstadoCosechas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoCosechas",
                table: "EstadoCosechas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cosechas_EstadoCosechas_IdEstadoCosecha",
                table: "Cosechas",
                column: "IdEstadoCosecha",
                principalTable: "EstadoCosechas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cosechas_EstadoCosechas_IdEstadoCosecha",
                table: "Cosechas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoCosechas",
                table: "EstadoCosechas");

            migrationBuilder.RenameTable(
                name: "EstadoCosechas",
                newName: "estadoCosechas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estadoCosechas",
                table: "estadoCosechas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cosechas_estadoCosechas_IdEstadoCosecha",
                table: "Cosechas",
                column: "IdEstadoCosecha",
                principalTable: "estadoCosechas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
