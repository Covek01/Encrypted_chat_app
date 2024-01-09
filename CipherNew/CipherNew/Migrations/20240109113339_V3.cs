using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CipherNew.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFile",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsFile",
                table: "Messages");
        }
    }
}
