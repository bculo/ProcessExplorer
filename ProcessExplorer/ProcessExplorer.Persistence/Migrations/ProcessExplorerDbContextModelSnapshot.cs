﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessExplorer.Persistence;

namespace ProcessExplorer.Persistence.Migrations
{
    [DbContext(typeof(ProcessExplorerDbContext))]
    partial class ProcessExplorerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

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

            modelBuilder.Entity("ProcessExplorer.Core.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Started")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Session");
                });
#pragma warning restore 612, 618
        }
    }
}
