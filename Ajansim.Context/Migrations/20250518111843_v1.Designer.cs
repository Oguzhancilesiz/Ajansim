﻿// <auto-generated />
using System;
using Ajansim.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ajansim.Context.Migrations
{
    [DbContext(typeof(AjansimDBContext))]
    [Migration("20250518111843_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ajansim.Entities.BlogPost", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("Ajansim.Entities.Category", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ajansim.Entities.ContactForm", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("ContactForms");
                });

            modelBuilder.Entity("Ajansim.Entities.FAQ", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("Ajansim.Entities.Media", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("BlogPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MediaType")
                        .HasColumnType("int");

                    b.Property<Guid?>("PageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PortfolioItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamMemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ID");

                    b.HasIndex("BlogPostId");

                    b.HasIndex("PageId");

                    b.HasIndex("PortfolioItemId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Ajansim.Entities.Page", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("MetaKeyword")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Ajansim.Entities.PortfolioItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamMemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("PortfolioItems");
                });

            modelBuilder.Entity("Ajansim.Entities.Service", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Ajansim.Entities.TeamMember", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GitHub")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Gmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LinkedIn")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("YouTube")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Ajansim.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ajansim.Entities.BlogPost", b =>
                {
                    b.HasOne("Ajansim.Entities.Category", "Category")
                        .WithMany("BlogPosts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ajansim.Entities.Media", b =>
                {
                    b.HasOne("Ajansim.Entities.BlogPost", "BlogPost")
                        .WithMany("MediaFiles")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ajansim.Entities.Page", "Page")
                        .WithMany("MediaFiles")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ajansim.Entities.PortfolioItem", "PortfolioItem")
                        .WithMany("MediaFiles")
                        .HasForeignKey("PortfolioItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ajansim.Entities.Service", "Service")
                        .WithMany("MediaFiles")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ajansim.Entities.TeamMember", "TeamMember")
                        .WithMany("MediaFiles")
                        .HasForeignKey("TeamMemberId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BlogPost");

                    b.Navigation("Page");

                    b.Navigation("PortfolioItem");

                    b.Navigation("Service");

                    b.Navigation("TeamMember");
                });

            modelBuilder.Entity("Ajansim.Entities.PortfolioItem", b =>
                {
                    b.HasOne("Ajansim.Entities.TeamMember", "TeamMember")
                        .WithMany("PortfolioItems")
                        .HasForeignKey("TeamMemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TeamMember");
                });

            modelBuilder.Entity("Ajansim.Entities.BlogPost", b =>
                {
                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("Ajansim.Entities.Category", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("Ajansim.Entities.Page", b =>
                {
                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("Ajansim.Entities.PortfolioItem", b =>
                {
                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("Ajansim.Entities.Service", b =>
                {
                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("Ajansim.Entities.TeamMember", b =>
                {
                    b.Navigation("MediaFiles");

                    b.Navigation("PortfolioItems");
                });
#pragma warning restore 612, 618
        }
    }
}
