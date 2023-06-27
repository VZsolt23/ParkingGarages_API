﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParkingGarages_API.Data;

#nullable disable

namespace ParkingGarages_API.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230626151209_DefaultDataInsertion")]
    partial class DefaultDataInsertion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ParkingGarages_API.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Piros",
                            Plate = "LOL-404",
                            Type = "Mazda CX-30"
                        },
                        new
                        {
                            Id = 2,
                            Color = "Fehér",
                            Plate = "BAD-400",
                            Type = "Toyota Corolla"
                        },
                        new
                        {
                            Id = 3,
                            Color = "Szürke",
                            Plate = "OKE-200",
                            Type = "Mercedes-Benz CLA 250"
                        });
                });

            modelBuilder.Entity("ParkingGarages_API.Models.Parking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("EndOfParking")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ParkingGarageId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("StartOfParking")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ParkingGarageId");

                    b.ToTable("Parkings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarId = 1,
                            EndOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)),
                            ParkingGarageId = 1,
                            StartOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CarId = 3,
                            EndOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)),
                            ParkingGarageId = 2,
                            StartOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0))
                        },
                        new
                        {
                            Id = 3,
                            CarId = 2,
                            EndOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)),
                            ParkingGarageId = 3,
                            StartOfParking = new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("ParkingGarages_API.Models.ParkingGarage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfSpaces")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ParkingGarages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Cím 1",
                            NumberOfSpaces = 10
                        },
                        new
                        {
                            Id = 2,
                            Address = "Cím 2",
                            NumberOfSpaces = 30
                        },
                        new
                        {
                            Id = 3,
                            Address = "Cím 3",
                            NumberOfSpaces = 15
                        });
                });

            modelBuilder.Entity("ParkingGarages_API.Models.Parking", b =>
                {
                    b.HasOne("ParkingGarages_API.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ParkingGarages_API.Models.ParkingGarage", "ParkingGarage")
                        .WithMany()
                        .HasForeignKey("ParkingGarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("ParkingGarage");
                });
#pragma warning restore 612, 618
        }
    }
}
