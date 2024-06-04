﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plugin.Repository.DbContexts;

namespace Plugin.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240604599999_initDatabase")]
    partial class initDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Plugin.Repository.Models.Effect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Effects");
                });

            modelBuilder.Entity("Plugin.Repository.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Radius")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Plugin.Repository.Models.ImageEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EffectId")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EffectId");

                    b.HasIndex("ImageId");

                    b.ToTable("ImageEffects");
                });

            modelBuilder.Entity("Plugin.Repository.Models.ImageEffect", b =>
                {
                    b.HasOne("Plugin.Repository.Models.Effect", "Effect")
                        .WithMany("ImageEffects")
                        .HasForeignKey("EffectId")
                        .HasConstraintName("FK_ImageEffects_Effects")
                        .IsRequired();

                    b.HasOne("Plugin.Repository.Models.Image", "Image")
                        .WithMany("ImageEffects")
                        .HasForeignKey("ImageId")
                        .HasConstraintName("FK_ImageEffects_Images")
                        .IsRequired();

                    b.Navigation("Effect");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Plugin.Repository.Models.Effect", b =>
                {
                    b.Navigation("ImageEffects");
                });

            modelBuilder.Entity("Plugin.Repository.Models.Image", b =>
                {
                    b.Navigation("ImageEffects");
                });
#pragma warning restore 612, 618
        }
    }
}