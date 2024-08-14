using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeForPets.Migrations
{
    /// <inheritdoc />
    public partial class ConversionBaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_details_pet_pet_id",
                table: "payment_details");

            migrationBuilder.DropForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_details_pets_pet_id",
                table: "payment_details",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photo_pets_pet_id",
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
                name: "fk_payment_details_pets_pet_id",
                table: "payment_details");

            migrationBuilder.DropForeignKey(
                name: "fk_pet_photo_pets_pet_id",
                table: "pet_photo");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_details_pet_pet_id",
                table: "payment_details",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
