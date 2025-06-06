﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ORM;

#nullable disable

namespace ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cashflowsystem_schema")
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.DailyBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("ClosingBalance")
                        .HasColumnType("numeric")
                        .HasColumnName("ClosingBalance");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("OpeningBalance")
                        .HasColumnType("numeric")
                        .HasColumnName("OpeningBalance");

                    b.Property<decimal>("TotalCredits")
                        .HasColumnType("numeric")
                        .HasColumnName("TotalCredits");

                    b.Property<decimal>("TotalDebits")
                        .HasColumnType("numeric")
                        .HasColumnName("TotalDebits");

                    b.Property<long>("TransactionCount")
                        .HasColumnType("bigint")
                        .HasColumnName("TransactionCount");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_DAILY_BALANCE");

                    b.HasIndex("MerchantId")
                        .HasDatabaseName("IX_DAILY_BALANCE_MERCHANT_ID");

                    b.ToTable("DailyBalance", "cashflowsystem_schema");
                });

            modelBuilder.Entity("Domain.Entities.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ContactEmail");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("ContactPhone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.Property<string>("TaxId")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("TaxId");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_MERCHANT");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_MERCHANT_NAME");

                    b.ToTable("Merchants", "cashflowsystem_schema");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("Currency");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uuid")
                        .HasColumnName("MerchantId");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("PaymentMethod");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_TRANSACTION");

                    b.HasIndex("MerchantId")
                        .HasDatabaseName("IX_TRANSACTION_MERCHANTID");

                    b.ToTable("Transactions", "cashflowsystem_schema");
                });

            modelBuilder.Entity("SharedKernel.Common.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("Phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasColumnName("Role");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(9)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Username");

                    b.HasKey("Id")
                        .HasName("PK_USER");

                    b.HasIndex("Username")
                        .HasDatabaseName("IX_USER_NAME");

                    b.ToTable("Users", "cashflowsystem_schema");
                });

            modelBuilder.Entity("Domain.Entities.DailyBalance", b =>
                {
                    b.HasOne("Domain.Entities.Merchant", "Merchant")
                        .WithMany("DailyBalances")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.Merchant", "Merchant")
                        .WithMany("Transactions")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Domain.Entities.Merchant", b =>
                {
                    b.Navigation("DailyBalances");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
