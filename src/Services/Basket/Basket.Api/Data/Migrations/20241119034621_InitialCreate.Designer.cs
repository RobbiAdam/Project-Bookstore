﻿// <auto-generated />
using System;
using Basket.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Basket.Api.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241119034621_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Basket.Api.Model.ShoppingCart", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Basket.Api.Model.ShoppingCart", b =>
                {
                    b.OwnsMany("Basket.Api.Model.ShoppingCartItem", "Items", b1 =>
                        {
                            b1.Property<string>("ShoppingCartUsername")
                                .HasColumnType("text");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("BookId")
                                .HasColumnType("uuid");

                            b1.Property<string>("BookTitle")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.HasKey("ShoppingCartUsername", "Id");

                            b1.ToTable("ShoppingCartItem");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartUsername");
                        });

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
