﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WiFiTracker.DB;

#nullable disable

namespace WiFiTracker.Migrations
{
    [DbContext(typeof(MainDB))]
    [Migration("20230212205727_AddedMultiAccounting")]
    partial class AddedMultiAccounting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WiFiTracker.DB.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("AccountLoginId")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WiFiTracker.DB.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("WiFiTracker.DB.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("WiFiTracker.DB.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TerminalId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Terminals");
                });

            modelBuilder.Entity("WiFiTracker.DB.Transmitter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<string>("Bssid")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Transmitters");
                });

            modelBuilder.Entity("WiFiTracker.DB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WiFiTracker.DB.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AccoundId")
                        .HasColumnType("int");

                    b.Property<bool>("AllowDownloadApp")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AllowReportLiveView")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AllowReportTrackHistory")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AllowTerminals")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AllowTransmitters")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
