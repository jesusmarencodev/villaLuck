﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VillaLuck.Datos;

#nullable disable

namespace VillaLuck.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VillaLuck.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle Villa 1",
                            FechaActualizacion = new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1140),
                            FechaCreacion = new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1127),
                            ImagenUrl = "",
                            MetrosCuadrados = 200,
                            Nombre = "Villa 1",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle Villa 2",
                            FechaActualizacion = new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1143),
                            FechaCreacion = new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1142),
                            ImagenUrl = "",
                            MetrosCuadrados = 300,
                            Nombre = "Villa 2",
                            Ocupantes = 7,
                            Tarifa = 300.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
