using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Futbol = table.Column<bool>(type: "bit", nullable: false),
                    Basquet = table.Column<bool>(type: "bit", nullable: false),
                    Otros = table.Column<bool>(type: "bit", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direccion_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_PersonaId",
                table: "Direccion",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
