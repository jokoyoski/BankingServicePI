﻿// <auto-generated />
using System;
using BankingAPI.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingAPI.Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("BankingAPI.Repository.Model.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardHolder")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpidrationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BankingAPI.Repository.Model.PaymentState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PaymentStates");
                });
#pragma warning restore 612, 618
        }
    }
}
