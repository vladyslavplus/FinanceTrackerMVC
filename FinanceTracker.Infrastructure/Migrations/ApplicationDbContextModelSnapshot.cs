﻿// <auto-generated />
using System;
using FinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanceTracker.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinanceTracker.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Icon = "🍔",
                            Title = "Food",
                            Type = "Expense"
                        },
                        new
                        {
                            Id = 2,
                            Icon = "💰",
                            Title = "Salary",
                            Type = "Income"
                        },
                        new
                        {
                            Id = 3,
                            Icon = "🚗",
                            Title = "Transport",
                            Type = "Expense"
                        },
                        new
                        {
                            Id = 4,
                            Icon = "🛍️",
                            Title = "Shopping",
                            Type = "Expense"
                        },
                        new
                        {
                            Id = 5,
                            Icon = "📈",
                            Title = "Investment",
                            Type = "Income"
                        });
                });

            modelBuilder.Entity("FinanceTracker.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50.75m,
                            CategoryId = 1,
                            Date = new DateTime(2025, 2, 5, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3973),
                            Note = "Lunch"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 2000.00m,
                            CategoryId = 2,
                            Date = new DateTime(2025, 1, 28, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3981),
                            Note = "Monthly salary"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 15.00m,
                            CategoryId = 3,
                            Date = new DateTime(2025, 2, 6, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3989),
                            Note = "Taxi ride"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 100.00m,
                            CategoryId = 4,
                            Date = new DateTime(2025, 2, 2, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3994),
                            Note = "New shoes"
                        },
                        new
                        {
                            Id = 5,
                            Amount = 300.00m,
                            CategoryId = 5,
                            Date = new DateTime(2025, 2, 4, 12, 12, 16, 187, DateTimeKind.Local).AddTicks(3998),
                            Note = "Stock purchase"
                        });
                });

            modelBuilder.Entity("FinanceTracker.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("FinanceTracker.Domain.Entities.Category", "Category")
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
