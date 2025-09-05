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
                name: "Korisnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sobe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaxBrojClanova = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciUSobama",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorsinikID = table.Column<int>(type: "int", nullable: false),
                    KorisnikFK = table.Column<int>(type: "int", nullable: true),
                    SobaID = table.Column<int>(type: "int", nullable: false),
                    SobaFK = table.Column<int>(type: "int", nullable: true),
                    Nadimak = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciUSobama", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KorisniciUSobama_Korisnici_KorisnikFK",
                        column: x => x.KorisnikFK,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_KorisniciUSobama_Sobe_SobaFK",
                        column: x => x.SobaFK,
                        principalTable: "Sobe",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUSobama_KorisnikFK",
                table: "KorisniciUSobama",
                column: "KorisnikFK");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUSobama_SobaFK",
                table: "KorisniciUSobama",
                column: "SobaFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciUSobama");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Sobe");
        }
    }
}
