﻿// <auto-generated />
using System;
using DetroitHarps.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DetroitHarps.DataAccess.Migrator.Migrations
{
    [DbContext(typeof(DetroitHarpsDbContext))]
    partial class DetroitHarpsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DetroitHarps.Business.Contact.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsRead");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<DateTimeOffset>("insert_timestamp");

                    b.Property<DateTimeOffset>("update_timestamp");

                    b.HasKey("Id");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("DetroitHarps.Business.Photo.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("insert_timestamp");

                    b.Property<DateTimeOffset>("update_timestamp");

                    b.HasKey("Id");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("DetroitHarps.Business.Photo.Entities.PhotoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<DateTimeOffset>("insert_timestamp");

                    b.Property<DateTimeOffset>("update_timestamp");

                    b.HasKey("Id");

                    b.ToTable("PhotoGroup");
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("RegistrationTimestamp");

                    b.Property<int>("SeasonYear");

                    b.Property<DateTimeOffset>("insert_timestamp");

                    b.Property<DateTimeOffset>("update_timestamp");

                    b.HasKey("Id");

                    b.ToTable("Registration");
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.RegistrationChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("RegistrationId");

                    b.Property<string>("ShirtSize")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("RegistrationChild");
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.RegistrationChildEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Answer");

                    b.Property<int>("EventId");

                    b.Property<int?>("RegistrationChildId");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationChildId");

                    b.ToTable("RegistrationChildEvent");
                });

            modelBuilder.Entity("DetroitHarps.Business.Schedule.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanRegister");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset>("insert_timestamp");

                    b.Property<DateTimeOffset>("update_timestamp");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("DetroitHarps.Business.Photo.Entities.Photo", b =>
                {
                    b.OwnsOne("DetroitHarps.Business.Photo.Entities.PhotoData", "Data", b1 =>
                        {
                            b1.Property<int>("PhotoId");

                            b1.Property<byte[]>("Data");

                            b1.Property<string>("MimeType")
                                .HasMaxLength(450);

                            b1.HasKey("PhotoId");

                            b1.ToTable("PhotoData");

                            b1.HasOne("DetroitHarps.Business.Photo.Entities.Photo")
                                .WithOne("Data")
                                .HasForeignKey("DetroitHarps.Business.Photo.Entities.PhotoData", "PhotoId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DetroitHarps.Business.Photo.Entities.PhotoDisplayProperties", "DisplayProperties", b1 =>
                        {
                            b1.Property<int>("PhotoId");

                            b1.Property<int>("PhotoGroupId");

                            b1.Property<int>("SortOrder");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.HasKey("PhotoId");

                            b1.HasIndex("PhotoGroupId");

                            b1.ToTable("Photo");

                            b1.HasOne("DetroitHarps.Business.Photo.Entities.PhotoGroup")
                                .WithMany()
                                .HasForeignKey("PhotoGroupId")
                                .OnDelete(DeleteBehavior.Restrict);

                            b1.HasOne("DetroitHarps.Business.Photo.Entities.Photo")
                                .WithOne("DisplayProperties")
                                .HasForeignKey("DetroitHarps.Business.Photo.Entities.PhotoDisplayProperties", "PhotoId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.Registration", b =>
                {
                    b.OwnsOne("DetroitHarps.Business.Registration.Entities.RegistrationContactInformation", "ContactInformation", b1 =>
                        {
                            b1.Property<int>("RegistrationId");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(450);

                            b1.Property<string>("Address2")
                                .HasMaxLength(450);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(20);

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("Zip")
                                .IsRequired()
                                .HasMaxLength(20);

                            b1.HasKey("RegistrationId");

                            b1.ToTable("Registration");

                            b1.HasOne("DetroitHarps.Business.Registration.Entities.Registration")
                                .WithOne("ContactInformation")
                                .HasForeignKey("DetroitHarps.Business.Registration.Entities.RegistrationContactInformation", "RegistrationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DetroitHarps.Business.Registration.Entities.RegistrationParent", "Parent", b1 =>
                        {
                            b1.Property<int>("RegistrationId");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.HasKey("RegistrationId");

                            b1.ToTable("Registration");

                            b1.HasOne("DetroitHarps.Business.Registration.Entities.Registration")
                                .WithOne("Parent")
                                .HasForeignKey("DetroitHarps.Business.Registration.Entities.RegistrationParent", "RegistrationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DetroitHarps.Business.Registration.Entities.RegistrationPaymentInformation", "PaymentInformation", b1 =>
                        {
                            b1.Property<int>("RegistrationId");

                            b1.Property<double>("Amount");

                            b1.Property<int>("PaymentType");

                            b1.HasKey("RegistrationId");

                            b1.ToTable("Registration");

                            b1.HasOne("DetroitHarps.Business.Registration.Entities.Registration")
                                .WithOne("PaymentInformation")
                                .HasForeignKey("DetroitHarps.Business.Registration.Entities.RegistrationPaymentInformation", "RegistrationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.RegistrationChild", b =>
                {
                    b.HasOne("DetroitHarps.Business.Registration.Entities.Registration")
                        .WithMany("Children")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("DetroitHarps.Business.Registration.Entities.RegistrationChildEvent", b =>
                {
                    b.HasOne("DetroitHarps.Business.Registration.Entities.RegistrationChild")
                        .WithMany("Events")
                        .HasForeignKey("RegistrationChildId");

                    b.OwnsOne("DetroitHarps.Business.Registration.Entities.RegistrationChildEventSnapshot", "EventSnapshot", b1 =>
                        {
                            b1.Property<int>("RegistrationChildEventId");

                            b1.Property<bool>("CanRegister");

                            b1.Property<string>("Description");

                            b1.Property<DateTime?>("EndDate");

                            b1.Property<DateTime>("StartDate");

                            b1.Property<string>("Title");

                            b1.HasKey("RegistrationChildEventId");

                            b1.ToTable("RegistrationChildEvent");

                            b1.HasOne("DetroitHarps.Business.Registration.Entities.RegistrationChildEvent")
                                .WithOne("EventSnapshot")
                                .HasForeignKey("DetroitHarps.Business.Registration.Entities.RegistrationChildEventSnapshot", "RegistrationChildEventId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
