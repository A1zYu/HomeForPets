using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeForPets.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    years_of_experience = table.Column<int>(type: "integer", nullable: false),
                    pet_home_found_count = table.Column<int>(type: "integer", nullable: false),
                    pet_search_for_home_count = table.Column<int>(type: "integer", nullable: false),
                    pet_treatment_count = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    full_name_first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    full_name_last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    full_name_middle_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    species = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    breed = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    color = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    health_info = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: false),
                    birth_of_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    help_status = table.Column<int>(type: "integer", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false),
                    address_city = table.Column<string>(type: "text", nullable: false),
                    address_district = table.Column<string>(type: "text", nullable: false),
                    address_flat_number = table.Column<int>(type: "integer", nullable: false),
                    address_house_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "social_networks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    path = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_networks", x => x.id);
                    table.ForeignKey(
                        name: "fk_social_networks_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "pet_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_photos_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
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

            migrationBuilder.CreateIndex(
                name: "ix_pet_photos_pet_id",
                table: "pet_photos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_networks_volunteer_id",
                table: "social_networks",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_details");

            migrationBuilder.DropTable(
                name: "pet_photos");

            migrationBuilder.DropTable(
                name: "social_networks");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
