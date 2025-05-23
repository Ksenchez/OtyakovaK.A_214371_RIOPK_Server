﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pricing_analyzer_back.Infrasctructure.Context;

#nullable disable

namespace shop_back.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250415170230_InitPA")]
    partial class InitPA
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.PricingPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DefaultMarkupPercent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PricingPolicies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DefaultMarkupPercent = 10m,
                            Description = "Базовая наценка 10%",
                            IsActive = true,
                            PolicyName = "Стандартная"
                        },
                        new
                        {
                            Id = 2,
                            DefaultMarkupPercent = 20m,
                            Description = "Увеличенная наценка 20%",
                            IsActive = true,
                            PolicyName = "Премиум"
                        },
                        new
                        {
                            Id = 3,
                            DefaultMarkupPercent = 5m,
                            Description = "Минимальная наценка 5%",
                            IsActive = true,
                            PolicyName = "Льготная"
                        });
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MarkupPercent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PricingPolicyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PricingPolicyId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BaseCost = 100m,
                            CreatedAt = new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9319),
                            Description = "Молоко 950мл.",
                            MarkupPercent = 15m,
                            Name = "Молоко"
                        },
                        new
                        {
                            Id = 2,
                            BaseCost = 200m,
                            CreatedAt = new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9321),
                            Description = "Масло 200гр.",
                            MarkupPercent = 10m,
                            Name = "Масло",
                            PricingPolicyId = 1
                        },
                        new
                        {
                            Id = 3,
                            BaseCost = 150m,
                            CreatedAt = new DateTime(2025, 4, 15, 17, 2, 28, 107, DateTimeKind.Utc).AddTicks(9323),
                            Description = "Coca-Cola 2л.",
                            MarkupPercent = 20m,
                            Name = "Coca-Cola",
                            PricingPolicyId = 2
                        });
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.ProductCalculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CalculatedAt")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CustomMarkup")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductCalculations");
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
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
                            PasswordHash = "$2a$11$pbB5Z8u9HKk0Ani/IIh60OIr4l3prpI4KgY1.j4XeBAN0mtdag/eO",
                            Role = "admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "$2a$11$EjJ0CWEUr6X6CmsF8aGBgu4h3tHid1SLHt.5wZJrvbMl9OZtrfTOS",
                            Role = "user",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.Product", b =>
                {
                    b.HasOne("pricing_analyzer_back.Infrasctructure.Models.PricingPolicy", "PricingPolicy")
                        .WithMany("Products")
                        .HasForeignKey("PricingPolicyId");

                    b.Navigation("PricingPolicy");
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.ProductCalculation", b =>
                {
                    b.HasOne("pricing_analyzer_back.Infrasctructure.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pricing_analyzer_back.Infrasctructure.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pricing_analyzer_back.Infrasctructure.Models.PricingPolicy", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
