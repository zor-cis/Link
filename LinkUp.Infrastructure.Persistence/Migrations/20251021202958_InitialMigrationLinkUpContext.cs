using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkUp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationLinkUpContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 10, 21, 16, 29, 55, 355, DateTimeKind.Local).AddTicks(5715)),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCommen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPublication = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCommen_Publications_IdPublication",
                        column: x => x.IdPublication,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublication = table.Column<int>(type: "int", nullable: false),
                    ReactionType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Publications_IdPublication",
                        column: x => x.IdPublication,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostCommen_IdPublication",
                table: "PostCommen",
                column: "IdPublication");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_IdPublication",
                table: "Reactions",
                column: "IdPublication");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCommen");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
