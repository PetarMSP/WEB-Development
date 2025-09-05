using System;
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
                name: "Sale",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projekcije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    VremePrikazivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salaID = table.Column<int>(type: "int", nullable: true),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OsnovnaCenaKarte = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekcije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projekcije_Sale_salaID",
                        column: x => x.salaID,
                        principalTable: "Sale",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSedista = table.Column<long>(type: "bigint", nullable: false),
                    Red = table.Column<long>(type: "bigint", nullable: false),
                    projekcijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_Projekcije_projekcijaID",
                        column: x => x.projekcijaID,
                        principalTable: "Projekcije",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karte_projekcijaID",
                table: "Karte",
                column: "projekcijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcije_salaID",
                table: "Projekcije",
                column: "salaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Projekcije");

            migrationBuilder.DropTable(
                name: "Sale");
        }
    }
}
