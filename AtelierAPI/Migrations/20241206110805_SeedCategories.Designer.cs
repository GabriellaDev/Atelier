﻿// <auto-generated />
using System;
using AtelierAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtelierAPI.Migrations
{
    [DbContext(typeof(AtelierContext))]
    [Migration("20241206110805_SeedCategories")]
    partial class SeedCategories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AtelierShared.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Concept Development"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Design & Prototyping"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Manufacturing & Production"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Sustainability & Green Technology"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Sustainability and ESG (Environmental, Social, Governance)"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Installation and Deployment"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Operational Efficiency"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Cost Optimization"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Safety and Risk Management"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Regulatory Compliance"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Customer-Focused Initiatives"
                        },
                        new
                        {
                            Id = 12,
                            Category = "-Digitalization and Data Management"
                        });
                });

            modelBuilder.Entity("AtelierShared.Models.InitiativePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("InitiativePosts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            CategoryId = 3,
                            Content = "Implement automated robotic systems for precision cutting and assembly of wind turbine blades. This will reduce production time by 20% and minimize material waste.",
                            DatePublished = new DateTime(2024, 12, 6, 11, 8, 4, 769, DateTimeKind.Utc).AddTicks(1788),
                            Title = "Improve Blade Manufacturing Efficiency"
                        });
                });

            modelBuilder.Entity("AtelierShared.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateJoined = new DateTime(2024, 12, 6, 11, 8, 4, 768, DateTimeKind.Utc).AddTicks(5366),
                            Email = "316333@viauc.dk",
                            Password = "hashed_password",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("AtelierShared.Models.InitiativePost", b =>
                {
                    b.HasOne("AtelierShared.Models.CategoryModel", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
