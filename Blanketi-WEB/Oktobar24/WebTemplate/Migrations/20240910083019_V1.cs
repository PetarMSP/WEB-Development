using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vaspitac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojUpisaneDece = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Deca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePrezime = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ImeRoditelja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    grupaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deca_Grupe_grupaID",
                        column: x => x.grupaID,
                        principalTable: "Grupe",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deca_grupaID",
                table: "Deca",
                column: "grupaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deca");

            migrationBuilder.DropTable(
                name: "Grupe");
        }
    }
}
