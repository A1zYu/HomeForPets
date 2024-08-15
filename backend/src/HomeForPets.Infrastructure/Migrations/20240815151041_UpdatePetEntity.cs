using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeForPets.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pet_photo_pets_pet_id",
                table: "pet_photo");

            migrationBuilder.DropTable(
                name: "payment_details");

            migrationBuilder.DropColumn(
                name: "contact_phone_number",
                table: "volunteers");

            migrationBuilder.RenameColumn(
                name: "contact_social_networks",
                table: "volunteers",
                newName: "payment_details_list");

            migrationBuilder.RenameColumn(
                name: "phone_number_owner_number",
                table: "pets",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "address_house_number",
                table: "pets",
                newName: "house_number");

            migrationBuilder.RenameColumn(
                name: "address_flat_number",
                table: "pets",
                newName: "flat_number");

            migrationBuilder.RenameColumn(
                name: "address_district",
                table: "pets",
                newName: "district");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "city");

            migrationBuilder.AddColumn<string>(
                name: "contact",
                table: "volunteers",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payment_details_list",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "pet_photos",
                table: "pet_photo",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "pet_photos",
                table: "pet_photo");

            migrationBuilder.DropColumn(
                name: "contact",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "payment_details_list",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "payment_details_list",
                table: "volunteers",
                newName: "contact_social_networks");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "pets",
                newName: "phone_number_owner_number");

            migrationBuilder.RenameColumn(
                name: "house_number",
                table: "pets",
                newName: "address_house_number");

            migrationBuilder.RenameColumn(
                name: "flat_number",
                table: "pets",
                newName: "address_flat_number");

            migrationBuilder.RenameColumn(
                name: "district",
                table: "pets",
                newName: "address_district");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "pets",
                newName: "address_city");

            migrationBuilder.AddColumn<string>(
                name: "contact_phone_number",
                table: "volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payment_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    name = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_details_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_details_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_details_pet_id",
                table: "payment_details",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_details_volunteer_id",
                table: "payment_details",
                column: "volunteer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photo_pets_pet_id",
                table: "pet_photo",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
