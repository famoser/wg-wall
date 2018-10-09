﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WgWall.Data;

namespace WgWall.Migrations.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20181009092959_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("WgWall.Data.Model.FrontendUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("FrontendUsers");
                });

            modelBuilder.Entity("WgWall.Data.Model.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("BoughtById");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BoughtById");

                    b.HasIndex("CreatedById");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WgWall.Data.Model.FrontendUser", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("WgWall.Data.Model.Products", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "BoughtBy")
                        .WithMany("BoughtProducts")
                        .HasForeignKey("BoughtById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });
#pragma warning restore 612, 618
        }
    }
}