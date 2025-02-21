﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using carpass.be.Infrastructure.Data;

#nullable disable

namespace CarSale.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230206075724_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarSale.Data.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarFeatureId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelYear")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarFeatureId");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarSale.Data.Entities.CarFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasAlarmSystem")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAutoParkingSystem")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAutomaticTransmission")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDigitalAc")
                        .HasColumnType("bit");

                    b.Property<bool>("HasElectricSideMirrors")
                        .HasColumnType("bit");

                    b.Property<bool>("HasHandsfreeMultimedia")
                        .HasColumnType("bit");

                    b.Property<bool>("HasKeylessDoors")
                        .HasColumnType("bit");

                    b.Property<bool>("HasKeylessStart")
                        .HasColumnType("bit");

                    b.Property<bool>("HasLeatherSeats")
                        .HasColumnType("bit");

                    b.Property<bool>("HasMultimedia")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPanoramicSunroof")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParkingSensor")
                        .HasColumnType("bit");

                    b.Property<bool>("HasSunroof")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CarFeatures");
                });

            modelBuilder.Entity("CarSale.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarSale.Data.Entities.Car", b =>
                {
                    b.HasOne("CarSale.Data.Entities.CarFeature", "CarFeature")
                        .WithMany()
                        .HasForeignKey("CarFeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarSale.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarFeature");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
