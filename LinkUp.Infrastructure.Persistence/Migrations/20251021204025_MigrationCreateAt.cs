using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkUp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationCreateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Publications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 10, 21, 16, 29, 55, 355, DateTimeKind.Local).AddTicks(5715));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Publications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 21, 16, 29, 55, 355, DateTimeKind.Local).AddTicks(5715),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
