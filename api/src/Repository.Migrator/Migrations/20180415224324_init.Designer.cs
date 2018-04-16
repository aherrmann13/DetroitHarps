﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Repository;
using System;

namespace Repository.Migrator.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20180415224324_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Repository.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Repository.Entities.PaymentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTimeOffset>("PaymentTimestamp");

                    b.Property<int>("RegisteredPersonId");

                    b.Property<string>("StripeCustomerId");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredPersonId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("Repository.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<int>("PhotoGroupId");

                    b.Property<int>("SortOrder");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PhotoGroupId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("Repository.Entities.PhotoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("PhotoGroup");
                });

            modelBuilder.Entity("Repository.Entities.RegisteredChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("RegisteredPersonId");

                    b.Property<string>("ShirtSize");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredPersonId");

                    b.ToTable("RegisteredChild");
                });

            modelBuilder.Entity("Repository.Entities.RegisteredPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("SeasonId");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("RegisteredPerson");
                });

            modelBuilder.Entity("Repository.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("Repository.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Repository.Entities.PaymentDetails", b =>
                {
                    b.HasOne("Repository.Entities.RegisteredPerson", "RegisteredPerson")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("RegisteredPersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Repository.Entities.Photo", b =>
                {
                    b.HasOne("Repository.Entities.PhotoGroup", "PhotoGroup")
                        .WithMany("Photos")
                        .HasForeignKey("PhotoGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Repository.Entities.RegisteredChild", b =>
                {
                    b.HasOne("Repository.Entities.RegisteredPerson", "RegisteredPerson")
                        .WithMany("Children")
                        .HasForeignKey("RegisteredPersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Repository.Entities.RegisteredPerson", b =>
                {
                    b.HasOne("Repository.Entities.Season", "Season")
                        .WithMany("RegisteredPeople")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
