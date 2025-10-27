using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkUp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationReplyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_PostCommen_PostCommenId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_PostCommenId",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "PostCommenId",
                table: "Reply");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_IdPostComment",
                table: "Reply",
                column: "IdPostComment");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_PostCommen_IdPostComment",
                table: "Reply",
                column: "IdPostComment",
                principalTable: "PostCommen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_PostCommen_IdPostComment",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_IdPostComment",
                table: "Reply");

            migrationBuilder.AddColumn<int>(
                name: "PostCommenId",
                table: "Reply",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reply_PostCommenId",
                table: "Reply",
                column: "PostCommenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_PostCommen_PostCommenId",
                table: "Reply",
                column: "PostCommenId",
                principalTable: "PostCommen",
                principalColumn: "Id");
        }
    }
}
