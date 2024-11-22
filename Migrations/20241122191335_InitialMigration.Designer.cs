﻿// <auto-generated />
using System;
using ContractMonthlyClaimSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContractMonthlyClaimSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241122191335_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContractMonthlyClaimSystem.Models.ClaimModel", b =>
                {
                    b.Property<int>("CLAIM_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CLAIM_ID"));

                    b.Property<int?>("APPROVER_ID")
                        .HasColumnType("int");

                    b.Property<string>("DOCUMENT_NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HOURLY_RATE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HOURS_WORKED")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LECTURER_ID")
                        .HasColumnType("int");

                    b.Property<string>("NOTES")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STATUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TIMESTAMP")
                        .HasColumnType("datetime2");

                    b.HasKey("CLAIM_ID");

                    b.ToTable("claimTBL", (string)null);
                });

            modelBuilder.Entity("ContractMonthlyClaimSystem.Models.cmcs_userTBLModel", b =>
                {
                    b.Property<int>("USERID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("USERID"));

                    b.Property<string>("ACCOUNT_TYPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ADDRESS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FULL_NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PASSWORD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PHONE_NUMBER")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("USERID");

                    b.ToTable("cmcs_userTBL", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}