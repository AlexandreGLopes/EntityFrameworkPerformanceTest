using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFPerformanceTest.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class Add_Graduations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graduation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccomplishDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graduation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonsGraduations",
                columns: table => new
                {
                    GraduationsId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false),
                    GraduationId = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsGraduations", x => new { x.GraduationsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_PersonsGraduations_Graduation_GraduationId",
                        column: x => x.GraduationId,
                        principalTable: "Graduation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonsGraduations_Graduation_GraduationsId",
                        column: x => x.GraduationsId,
                        principalTable: "Graduation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsGraduations_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonsGraduations_Person_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonsGraduations_GraduationId",
                table: "PersonsGraduations",
                column: "GraduationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsGraduations_PersonId",
                table: "PersonsGraduations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsGraduations_PersonsId",
                table: "PersonsGraduations",
                column: "PersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsGraduations");

            migrationBuilder.DropTable(
                name: "Graduation");
        }
    }
}
