﻿// <auto-generated />
using System;
using CRM.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240403142210_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRM.Domain.Entities.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<long>("MarketerId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MarketerId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Lead", b =>
                {
                    b.Property<long>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SellerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ContactId");

                    b.HasIndex("SellerId");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Sale", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("LeadId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SellerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SoldDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LeadId");

                    b.HasIndex("SellerId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("CRM.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("BlockDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Contact", b =>
                {
                    b.HasOne("CRM.Domain.Entities.User", "Marketer")
                        .WithMany()
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marketer");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Lead", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Contact", "Contact")
                        .WithOne()
                        .HasForeignKey("CRM.Domain.Entities.Lead", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.Navigation("Contact");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Sale", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Lead", "Lead")
                        .WithMany()
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.Navigation("Lead");

                    b.Navigation("Seller");
                });
#pragma warning restore 612, 618
        }
    }
}
