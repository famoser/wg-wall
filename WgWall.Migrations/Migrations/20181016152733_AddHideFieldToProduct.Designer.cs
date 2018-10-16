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
    [Migration("20181016152733_AddHideFieldToProduct")]
    partial class AddHideFieldToProduct
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

                    b.Property<int>("Karma");

                    b.Property<string>("Name");

                    b.Property<string>("ProfileImageSrc");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("FrontendUsers");
                });

            modelBuilder.Entity("WgWall.Data.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("BoughtById");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<bool>("Hide");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BoughtById");

                    b.HasIndex("CreatedById");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WgWall.Data.Model.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("WgWall.Data.Model.FrontendUser", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("WgWall.Data.Model.Product", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "BoughtBy")
                        .WithMany("BoughtProducts")
                        .HasForeignKey("BoughtById");

                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });
#pragma warning restore 612, 618
        }
    }
}
