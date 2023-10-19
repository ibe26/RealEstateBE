﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstateBE.Data;

#nullable disable

namespace RealEstateBE.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstateBE.Model.Property", b =>
                {
                    b.Property<int>("PropertyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyID"));

                    b.Property<short>("BathroomCount")
                        .HasColumnType("smallint");

                    b.Property<short>("BedroomCount")
                        .HasColumnType("smallint");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyListingType")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyPrice")
                        .HasColumnType("int");

                    b.Property<int>("PropertyType")
                        .HasColumnType("int");

                    b.Property<string>("Quarter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("PropertyID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstateBE.Model.PropertyListingType", b =>
                {
                    b.Property<int>("PropertyListingTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyListingTypeID"));

                    b.Property<string>("PropertyListingTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyListingTypeID");

                    b.ToTable("PropertyListingTypes");
                });

            modelBuilder.Entity("RealEstateBE.Model.PropertyType", b =>
                {
                    b.Property<int>("PropertyTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyTypeID"));

                    b.Property<string>("PropertyTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyTypeID");

                    b.ToTable("PropertyTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
