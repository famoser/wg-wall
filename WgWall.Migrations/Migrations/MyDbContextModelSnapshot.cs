﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WgWall.Data;

namespace WgWall.Migrations.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("WgWall.Data.Model.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivatedAt");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<int?>("DoneById");

                    b.Property<int>("TaskTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DoneById");

                    b.HasIndex("TaskTemplateId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("WgWall.Data.Model.TaskTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<bool>("Hide");

                    b.Property<int?>("IntervalInDays");

                    b.Property<DateTime?>("LastActivationAt");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("TaskTemplates");
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

            modelBuilder.Entity("WgWall.Data.Model.Task", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("WgWall.Data.Model.FrontendUser", "DoneBy")
                        .WithMany()
                        .HasForeignKey("DoneById");

                    b.HasOne("WgWall.Data.Model.TaskTemplate", "TaskTemplate")
                        .WithMany()
                        .HasForeignKey("TaskTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WgWall.Data.Model.TaskTemplate", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });
#pragma warning restore 612, 618
        }
    }
}
