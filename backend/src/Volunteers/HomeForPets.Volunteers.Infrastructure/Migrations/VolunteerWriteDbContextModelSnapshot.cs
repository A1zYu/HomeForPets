﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HomeForPets.Volunteers.Infrastucture.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeForPets.Volunteers.Infrastucture.Migrations
{
    [DbContext(typeof(VolunteerWriteDbContext))]
    partial class VolunteerWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("volunteers")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("create_date");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("PaymentDetails")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("payment_details");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "HomeForPets.Volunteers.Domain.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<int>("FlatNumber")
                                .HasColumnType("integer")
                                .HasColumnName("flat_number");

                            b1.Property<int>("HouseNumber")
                                .HasColumnType("integer")
                                .HasColumnName("house_number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "HomeForPets.Volunteers.Domain.Entities.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetDetails", "HomeForPets.Volunteers.Domain.Entities.Pet.PetDetails#PetDetails", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumberOwner", "HomeForPets.Volunteers.Domain.Entities.Pet.PhoneNumberOwner#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreed", "HomeForPets.Volunteers.Domain.Entities.Pet.SpeciesBreed#SpeciesBreed", b1 =>
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

                    b.ToTable("pets", "volunteers");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Entities.PetPhoto", b =>
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

                    b.ToTable("pet_photos", "volunteers");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("PaymentDetails")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("payment_details");

                    b.Property<string>("SocialNetworks")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("social_networks");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "HomeForPets.Volunteers.Domain.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "HomeForPets.Volunteers.Domain.Volunteer.FullName#FullName", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "HomeForPets.Volunteers.Domain.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("YearsOfExperience", "HomeForPets.Volunteers.Domain.Volunteer.YearsOfExperience#YearsOfExperience", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int?>("Value")
                                .IsRequired()
                                .HasColumnType("integer")
                                .HasColumnName("years_of_experience");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", "volunteers");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Entities.Pet", b =>
                {
                    b.HasOne("HomeForPets.Volunteers.Domain.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Entities.PetPhoto", b =>
                {
                    b.HasOne("HomeForPets.Volunteers.Domain.Entities.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_photos_pets_pet_id");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Entities.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("HomeForPets.Volunteers.Domain.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
