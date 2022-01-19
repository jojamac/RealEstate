using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class removedcolumnrefreshtokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "Identity",
                table: "RefreshToken",
                columns: new[] { "ApplicationUserId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Identity",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "Identity",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "Identity",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
