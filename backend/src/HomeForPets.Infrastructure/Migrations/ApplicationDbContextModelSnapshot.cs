﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HomeForPets.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeForPets.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeForPets.Domain.Species.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<Guid?>("SpeciesId")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breed");

                    b.HasIndex("SpeciesId")
                        .HasDatabaseName("ix_breed_species_id");

                    b.ToTable("breed", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Domain.Species.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "HomeForPets.Domain.Volunteers.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("district");

                            b1.Property<int>("FlatNumber")
                                .HasColumnType("integer")
                                .HasColumnName("flat_number");

                            b1.Property<int>("HouseNumber")
                                .HasColumnType("integer")
                                .HasColumnName("house_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "HomeForPets.Domain.Volunteers.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetDetails", "HomeForPets.Domain.Volunteers.Pet.PetDetails#PetDetails", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("BirthOfDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("birth_of_date");

                            b1.Property<string>("Color")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("color");

                            b1.Property<string>("HealthInfo")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("health_info");

                            b1.Property<double>("Height")
                                .HasColumnType("double precision")
                                .HasColumnName("height");

                            b1.Property<bool>("IsNeutered")
                                .HasColumnType("boolean")
                                .HasColumnName("is_neutered");

                            b1.Property<bool>("IsVaccinated")
                                .HasColumnType("boolean")
                                .HasColumnName("is_vaccinated");

                            b1.Property<double>("Weight")
                                .HasColumnType("double precision")
                                .HasColumnName("weight");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumberOwner", "HomeForPets.Domain.Volunteers.Pet.PhoneNumberOwner#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreed", "HomeForPets.Domain.Volunteers.Pet.SpeciesBreed#SpeciesBreed", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
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
                        .HasName("pk_pet_photos");

                    b.HasIndex("PetId")
                        .HasDatabaseName("ix_pet_photos_pet_id");

                    b.ToTable("pet_photos", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "HomeForPets.Domain.Volunteers.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "HomeForPets.Domain.Volunteers.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("last_name");

                            b1.Property<string>("MiddleName")
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("middle_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "HomeForPets.Domain.Volunteers.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("YearsOfExperience", "HomeForPets.Domain.Volunteers.Volunteer.YearsOfExperience#YearsOfExperience", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int?>("Value")
                                .IsRequired()
                                .HasColumnType("integer")
                                .HasColumnName("years_of_experience");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("HomeForPets.Domain.Species.Breed", b =>
                {
                    b.HasOne("HomeForPets.Domain.Species.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_breed_species_species_id");
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Pet", b =>
                {
                    b.HasOne("HomeForPets.Domain.Volunteers.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("HomeForPets.Domain.Shared.ValueObjects.PaymentDetailsList", "PaymentDetailsList", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("payment_details");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("HomeForPets.Domain.Shared.ValueObjects.PaymentDetails", "PaymentDetails", b2 =>
                                {
                                    b2.Property<Guid>("PaymentDetailsListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("payment_details_description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("payment_details_name");

                                    b2.HasKey("PaymentDetailsListPetId", "Id");

                                    b2.ToTable("pets");

                                    b2.ToJson("payment_details");

                                    b2.WithOwner()
                                        .HasForeignKey("PaymentDetailsListPetId")
                                        .HasConstraintName("fk_pets_pets_payment_details_list_pet_id");
                                });

                            b1.Navigation("PaymentDetails");
                        });

                    b.Navigation("PaymentDetailsList")
                        .IsRequired();
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.PetPhoto", b =>
                {
                    b.HasOne("HomeForPets.Domain.Volunteers.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_photos_pets_pet_id");
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Volunteer", b =>
                {
                    b.OwnsOne("HomeForPets.Domain.Volunteers.SocialNetworkList", "SocialNetworkList", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("social_network");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("HomeForPets.Domain.Volunteers.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("social_network_name");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("social_network_path");

                                    b2.HasKey("SocialNetworkListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("social_network");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_list_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.OwnsOne("HomeForPets.Domain.Shared.ValueObjects.PaymentDetailsList", "PaymentDetailsList", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("payment_details");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("HomeForPets.Domain.Shared.ValueObjects.PaymentDetails", "PaymentDetails", b2 =>
                                {
                                    b2.Property<Guid>("PaymentDetailsListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("payment_details_description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("payment_details_name");

                                    b2.HasKey("PaymentDetailsListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("payment_details");

                                    b2.WithOwner()
                                        .HasForeignKey("PaymentDetailsListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_payment_details_list_volunteer_id");
                                });

                            b1.Navigation("PaymentDetails");
                        });

                    b.Navigation("PaymentDetailsList");

                    b.Navigation("SocialNetworkList");
                });

            modelBuilder.Entity("HomeForPets.Domain.Species.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("HomeForPets.Domain.Volunteers.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
