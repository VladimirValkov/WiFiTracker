﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WiFiTracker.DB;

namespace WiFiTracker.Migrations
{
    [DbContext(typeof(MainDB))]
    [Migration("20221016183417_AddTerminals")]
    partial class AddTerminals
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("WiFiTracker.DB.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TerminalId")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Terminals");
                });
#pragma warning restore 612, 618
        }
    }
}
