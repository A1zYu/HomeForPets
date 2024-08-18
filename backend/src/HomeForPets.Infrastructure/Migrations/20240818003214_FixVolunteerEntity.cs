using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeForPets.Migrations
{
    /// <inheritdoc />
    public partial class FixVolunteerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "full_name_middle_name",
                table: "volunteers",
                newName: "middle_name");

            migrationBuilder.RenameColumn(
                name: "full_name_last_name",
                table: "volunteers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "full_name_first_name",
                table: "volunteers",
                newName: "first_name");

            migrationBuilder.AlterColumn<string>(
                name: "middle_name",
                table: "volunteers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "middle_name",
                table: "volunteers",
                newName: "full_name_middle_name");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "volunteers",
                newName: "full_name_last_name");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "volunteers",
                newName: "full_name_first_name");

            migrationBuilder.AlterColumn<string>(
                name: "full_name_middle_name",
                table: "volunteers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
