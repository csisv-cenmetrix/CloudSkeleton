﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskAPI.DataAccess;

namespace TaskAPI.DataAccess.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20210823070455_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskAPI.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressNo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("JobRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressNo = "45",
                            City = "Belgium",
                            FullName = "Trevor	Duncan",
                            JobRole = "Developer",
                            Street = "Street 1"
                        },
                        new
                        {
                            Id = 2,
                            AddressNo = "35",
                            City = "Brazil",
                            FullName = "Carol	McLean",
                            JobRole = "Systems Engineer",
                            Street = "Street 2"
                        },
                        new
                        {
                            Id = 3,
                            AddressNo = "25",
                            City = "Netherlands",
                            FullName = "Alexander	Pullman",
                            JobRole = "Developer",
                            Street = "Street 3"
                        },
                        new
                        {
                            Id = 4,
                            AddressNo = "15",
                            City = "Ukraine",
                            FullName = "Adrian	Parr",
                            JobRole = "QA",
                            Street = "Street 4"
                        });
                });

            modelBuilder.Entity("TaskAPI.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("Due")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Todos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Created = new DateTime(2021, 8, 23, 12, 34, 55, 225, DateTimeKind.Local).AddTicks(630),
                            Description = "Get some text books for school",
                            Due = new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(3719),
                            Status = 0,
                            Title = "Get books for school - DB"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Created = new DateTime(2021, 8, 23, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4578),
                            Description = "Go to supermarket and by some stuff",
                            Due = new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4583),
                            Status = 0,
                            Title = "Need some grocceries"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Created = new DateTime(2021, 8, 23, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4589),
                            Description = "Buy new camera",
                            Due = new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4590),
                            Status = 0,
                            Title = "Purchase Camera"
                        });
                });

            modelBuilder.Entity("TaskAPI.Models.Todo", b =>
                {
                    b.HasOne("TaskAPI.Models.Author", "Author")
                        .WithMany("Todos")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("TaskAPI.Models.Author", b =>
                {
                    b.Navigation("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
