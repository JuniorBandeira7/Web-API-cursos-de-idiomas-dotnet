using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Corrigindo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurma");

            migrationBuilder.CreateTable(
                name: "alunoTurmas",
                columns: table => new
                {
                    AlunoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TurmaId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunoTurmas", x => new { x.AlunoId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_alunoTurmas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alunoTurmas_turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turmas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_alunoTurmas_TurmaId",
                table: "alunoTurmas",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alunoTurmas");

            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    AlunoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TurmaId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => new { x.AlunoId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turmas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_TurmaId",
                table: "AlunoTurma",
                column: "TurmaId");
        }
    }
}
