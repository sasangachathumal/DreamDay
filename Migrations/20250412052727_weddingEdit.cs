using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamDay.Migrations
{
    /// <inheritdoc />
    public partial class weddingEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weddings_AspNetUsers_PlannerId",
                table: "Weddings");

            migrationBuilder.AlterColumn<string>(
                name: "PlannerId",
                table: "Weddings",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Weddings_AspNetUsers_PlannerId",
                table: "Weddings",
                column: "PlannerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weddings_AspNetUsers_PlannerId",
                table: "Weddings");

            migrationBuilder.UpdateData(
                table: "Weddings",
                keyColumn: "PlannerId",
                keyValue: null,
                column: "PlannerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PlannerId",
                table: "Weddings",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Weddings_AspNetUsers_PlannerId",
                table: "Weddings",
                column: "PlannerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
