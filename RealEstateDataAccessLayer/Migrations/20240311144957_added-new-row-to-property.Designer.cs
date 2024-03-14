﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstateDataAccessLayer.Data;

#nullable disable

namespace RealEstateDataAccessLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240311144957_added-new-row-to-property")]
    partial class addednewrowtoproperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstateEntities.Entities.OwnedProperty", b =>
                {
                    b.Property<int>("PropertyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyID"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("GrossArea")
                        .HasColumnType("int");

                    b.Property<int>("GrossIncome")
                        .HasColumnType("int");

                    b.Property<int>("NetArea")
                        .HasColumnType("int");

                    b.Property<int>("NetIncome")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyPrice")
                        .HasColumnType("int");

                    b.Property<int>("PropertyTypeID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PropertyID");

                    b.HasIndex("PropertyTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("OwnedProperties");
                });

            modelBuilder.Entity("RealEstateEntities.Entities.Property", b =>
                {
                    b.Property<int>("PropertyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyID"));

                    b.Property<bool>("Balcony")
                        .HasColumnType("bit");

                    b.Property<short>("BathroomCount")
                        .HasColumnType("smallint");

                    b.Property<short>("BedroomCount")
                        .HasColumnType("smallint");

                    b.Property<short>("BuiltYear")
                        .HasColumnType("smallint");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateListed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dues")
                        .HasColumnType("int");

                    b.Property<short?>("Floor")
                        .HasColumnType("smallint");

                    b.Property<int>("GrossArea")
                        .HasColumnType("int");

                    b.Property<string>("HeatSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NetArea")
                        .HasColumnType("int");

                    b.Property<bool>("OnListing")
                        .HasColumnType("bit");

                    b.Property<int>("PropertyListingTypeID")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyPrice")
                        .HasColumnType("int");

                    b.Property<int>("PropertyTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Quarter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("TotalFloor")
                        .HasColumnType("smallint");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PropertyID");

                    b.HasIndex("PropertyListingTypeID");

                    b.HasIndex("PropertyTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstateEntities.Entities.PropertyListingType", b =>
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

            modelBuilder.Entity("RealEstateEntities.Entities.PropertyType", b =>
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

            modelBuilder.Entity("RealEstateEntities.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RealEstateEntities.Entities.OwnedProperty", b =>
                {
                    b.HasOne("RealEstateEntities.Entities.PropertyType", "PropertyType")
                        .WithMany()
                        .HasForeignKey("PropertyTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateEntities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealEstateEntities.Entities.Property", b =>
                {
                    b.HasOne("RealEstateEntities.Entities.PropertyListingType", "PropertyListingType")
                        .WithMany()
                        .HasForeignKey("PropertyListingTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateEntities.Entities.PropertyType", "PropertyType")
                        .WithMany()
                        .HasForeignKey("PropertyTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateEntities.Entities.User", null)
                        .WithMany("ListedProperties")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyListingType");

                    b.Navigation("PropertyType");
                });

            modelBuilder.Entity("RealEstateEntities.Entities.User", b =>
                {
                    b.Navigation("ListedProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
