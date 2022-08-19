﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _01.Net_Core_Restful_API.Data;

namespace _01.Net_Core_Restful_API.Migrations
{
    [DbContext(typeof(RoutineDbContext))]
    [Migration("20210422112611_AddEmployee")]
    partial class AddEmployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("_01.Net_Core_Restful_API.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Introduction")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                            Introduction = "Great Compant",
                            Name = "Google"
                        },
                        new
                        {
                            Id = new Guid("5001e311-51d2-469f-8446-ba5364155f29"),
                            Introduction = "Don't be evil",
                            Name = "Microsoft"
                        },
                        new
                        {
                            Id = new Guid("5313d3dc-874c-4b46-a8b8-2018923213fb"),
                            Introduction = "TaoBao Company",
                            Name = "Alipapa"
                        });
                });

            modelBuilder.Entity("_01.Net_Core_Restful_API.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("07389c3c-ac3e-46f9-b6bd-5320b7eb326d"),
                            CompanyId = new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                            DateOfBirth = new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "cm20210001",
                            FirstName = "Nick",
                            Gender = 1,
                            LastName = "Brand"
                        },
                        new
                        {
                            Id = new Guid("5e23cc44-50f3-4ef7-976b-92e0991899b8"),
                            CompanyId = new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                            DateOfBirth = new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "cm20210002",
                            FirstName = "Vince",
                            Gender = 2,
                            LastName = "John"
                        },
                        new
                        {
                            Id = new Guid("a9061730-7868-4869-9f96-f17d5ac97690"),
                            CompanyId = new Guid("84c8ff0a-90a7-4f54-b47b-4c933f4f2115"),
                            DateOfBirth = new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "cm20210003",
                            FirstName = "Bryant",
                            Gender = 1,
                            LastName = "Kobbo"
                        });
                });

            modelBuilder.Entity("_01.Net_Core_Restful_API.Entities.Employee", b =>
                {
                    b.HasOne("_01.Net_Core_Restful_API.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("_01.Net_Core_Restful_API.Entities.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}