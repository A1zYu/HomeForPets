﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HomeForPets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeForPets.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240814095709_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeForPets.Models.PaymentDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("name");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_payment_details");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_payment_details_pet_id");

                    b.HasIndex("VolunteerId")
                        .HasDatabaseName("ix_payment_details_volunteer_id");

                    b.ToTable("payment_details", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthOfDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_of_date");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("color");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("description");

                    b.Property<string>("HealthInfo")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("health_info");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("species");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "HomeForPets.Models.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_city");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_district");

                            b1.Property<int>("FlatNumber")
                                .HasColumnType("integer")
                                .HasColumnName("address_flat_number");

                            b1.Property<int>("HouseNumber")
                                .HasColumnType("integer")
                                .HasColumnName("address_house_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pet");

                    b.HasIndex("VolunteerId")
                        .HasDatabaseName("ix_pet_volunteer_id");

                    b.ToTable("pet", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Models.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("path");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_pet_photo");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_pet_photo_pet_id");

                    b.ToTable("pet_photo", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Models.SocialNetwork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("path");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_social_network");

                    b.HasIndex("VolunteerId")
                        .HasDatabaseName("ix_social_network_volunteer_id");

                    b.ToTable("social_network", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("description");

                    b.Property<int>("PetHomeFoundCount")
                        .HasColumnType("integer")
                        .HasColumnName("pet_home_found_count");

                    b.Property<int>("PetSearchForHomeCount")
                        .HasColumnType("integer")
                        .HasColumnName("pet_search_for_home_count");

                    b.Property<int>("PetTreatmentCount")
                        .HasColumnType("integer")
                        .HasColumnName("pet_treatment_count");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("phone_number");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("integer")
                        .HasColumnName("years_of_experience");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "HomeForPets.Models.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("full_name_last_name");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("full_name_middle_name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Models.PaymentDetails", b =>
                {
                    b.HasOne("HomeForPets.Models.Pet", null)
                        .WithMany("PaymentDetailsList")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_payment_details_pet_pet_id");

                    b.HasOne("HomeForPets.Models.Volunteer", null)
                        .WithMany("PaymentDetailsList")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_payment_details_volunteers_volunteer_id");
                });

            modelBuilder.Entity("HomeForPets.Models.Pet", b =>
                {
                    b.HasOne("HomeForPets.Models.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_volunteers_volunteer_id");
                });

            modelBuilder.Entity("HomeForPets.Models.PetPhoto", b =>
                {
                    b.HasOne("HomeForPets.Models.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_photo_pet_pet_id");
                });

            modelBuilder.Entity("HomeForPets.Models.SocialNetwork", b =>
                {
                    b.HasOne("HomeForPets.Models.Volunteer", null)
                        .WithMany("SocialNetworks")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_social_network_volunteers_volunteer_id");
                });

            modelBuilder.Entity("HomeForPets.Models.Pet", b =>
                {
                    b.Navigation("PaymentDetailsList");

                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("HomeForPets.Models.Volunteer", b =>
                {
                    b.Navigation("PaymentDetailsList");

                    b.Navigation("Pets");

                    b.Navigation("SocialNetworks");
                });
#pragma warning restore 612, 618
        }
    }
}
