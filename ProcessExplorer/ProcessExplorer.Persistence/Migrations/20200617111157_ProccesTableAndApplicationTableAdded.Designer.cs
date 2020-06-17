﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessExplorer.Persistence;

namespace ProcessExplorer.Persistence.Migrations
{
    [DbContext(typeof(ProcessExplorerDbContext))]
    [Migration("20200617111157_ProccesTableAndApplicationTableAdded")]
    partial class ProccesTableAndApplicationTableAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("ProcessExplorer.Core.Entities.ApplicationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(300);

                    b.Property<DateTime>("Saved")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("ApplicationEntity");
                });

            modelBuilder.Entity("ProcessExplorer.Core.Entities.Authentication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("Inserted")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("ProcessExplorer.Core.Entities.ProcessEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProcessId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProcessName")
                        .HasColumnType("INTEGER")
                        .HasMaxLength(300);

                    b.Property<DateTime>("Saved")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("ProcessEntity");
                });

            modelBuilder.Entity("ProcessExplorer.Core.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Started")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("ProcessExplorer.Core.Entities.ApplicationEntity", b =>
                {
                    b.HasOne("ProcessExplorer.Core.Entities.Session", "Session")
                        .WithMany("Applications")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ProcessExplorer.Core.Entities.ProcessEntity", b =>
                {
                    b.HasOne("ProcessExplorer.Core.Entities.Session", "Session")
                        .WithMany("ProcessEntities")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
