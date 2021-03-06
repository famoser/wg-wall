﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

            modelBuilder.Entity("WgWall.Data.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("WgWall.Data.Model.FrontendUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Karma");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("FrontendUsers");
                });

            modelBuilder.Entity("WgWall.Data.Model.Plate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountableId");

                    b.Property<int>("DinnerState");

                    b.Property<DateTime>("ValidityDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountableId");

                    b.ToTable("Plates");
                });

            modelBuilder.Entity("WgWall.Data.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<bool>("IsHidden");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WgWall.Data.Model.ProductPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountableId");

                    b.Property<int?>("EntityId");

                    b.Property<DateTime>("ExecutedAt");

                    b.Property<int>("KarmaEarned");

                    b.HasKey("Id");

                    b.HasIndex("AccountableId");

                    b.HasIndex("EntityId");

                    b.ToTable("ProductPurchases");
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

            modelBuilder.Entity("WgWall.Data.Model.TaskExecution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountableId");

                    b.Property<int?>("EntityId");

                    b.Property<DateTime>("ExecutedAt");

                    b.Property<int>("KarmaEarned");

                    b.HasKey("Id");

                    b.HasIndex("AccountableId");

                    b.HasIndex("EntityId");

                    b.ToTable("TaskExecutions");
                });

            modelBuilder.Entity("WgWall.Data.Model.TaskTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IntervalInDays");

                    b.Property<bool>("IsHidden");

                    b.Property<DateTime?>("LastExecutionAt");

                    b.Property<string>("Name");

                    b.Property<int>("Reward");

                    b.HasKey("Id");

                    b.ToTable("TaskTemplates");
                });

            modelBuilder.Entity("WgWall.Data.Model.Plate", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "Accountable")
                        .WithMany()
                        .HasForeignKey("AccountableId");
                });

            modelBuilder.Entity("WgWall.Data.Model.ProductPurchase", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "Accountable")
                        .WithMany("PurchasedProducts")
                        .HasForeignKey("AccountableId");

                    b.HasOne("WgWall.Data.Model.Product", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId");
                });

            modelBuilder.Entity("WgWall.Data.Model.TaskExecution", b =>
                {
                    b.HasOne("WgWall.Data.Model.FrontendUser", "Accountable")
                        .WithMany("ExecutedTasks")
                        .HasForeignKey("AccountableId");

                    b.HasOne("WgWall.Data.Model.TaskTemplate", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
