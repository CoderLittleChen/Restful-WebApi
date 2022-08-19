using _01.Net_Core_Restful_API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(p => p.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>().Property(p => p.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                        .HasOne(h => h.Company)
                        .WithMany(w => w.Employees)
                        .HasForeignKey(h => h.CompanyId).OnDelete(DeleteBehavior.Cascade);

            //设置初始数据
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("84C8FF0A-90A7-4F54-B47B-4C933F4F2115"),
                    Name = "Google",
                    Country = "America",
                    Industry = "Android",
                    Product="Google Chrome Browser",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("5001E311-51D2-469F-8446-BA5364155F29"),
                    Name = "Microsoft",
                    Country= "America",
                    Industry="PC System",
                    Product="Office 365",
                    Introduction = "Don't be evil"
                },
                new Company
                {
                    Id = Guid.Parse("5313D3DC-874C-4B46-A8B8-2018923213FB"),
                    Name = "Alipapa",
                    Country="China",
                    Industry="Internet",
                    Product="AliPay",
                    Introduction = "TaoBao Company"
                },
                new Company
                {
                    Id = Guid.Parse("B3A3A73E-88C0-4C94-AF92-C9D11B0A8B9C"),
                    Name = "ByteDance",
                    Country="China",
                    Industry="Short Video",
                    Product= "Tik Tok",
                    Introduction = "Tik Tok"
                },
                new Company
                {
                    Id = Guid.Parse("1A805447-13DB-41C8-861E-370942215B96"),
                    Name = "XIOAMI",
                    Country="China",
                    Industry="Mobile Phone",
                    Product="Xiao Mi 11",
                    Introduction = "Mobile Phone Company"
                },
                new Company
                {
                    Id = Guid.Parse("F322D130-6E24-477E-8F49-684BFC681211"),
                    Name = "360",
                    Country="China",
                    Industry="Safe Software",
                    Product="360",
                    Introduction = "Software Company"
                },
                new Company
                {
                    Id = Guid.Parse("50D80F33-844D-4D28-9504-7E541719FF36"),
                    Name = "Tencent",
                    Country = "China",
                    Industry="Social APP,Game",
                    Product="WeChat,QQ",
                    Introduction = "Game Company"
                },
                new Company
                {
                    Id = Guid.Parse("423D5D98-A496-44D8-8008-BC0B9C40FD80"),
                    Name = "Douyu",
                    Country = "China",
                    Industry="Live",
                    Product="Douyu Live",
                    Introduction = "Live WebSite"
                },
                new Company
                {
                    Id = Guid.Parse("B230F808-1CAD-47EA-A484-68CCB3D53154"),
                    Name = "Twitter",
                    Country = "America",
                    Industry="Social",
                    Product="Twitter",
                    Introduction = "Social App"
                },
                new Company
                {
                    Id = Guid.Parse("F337A186-D3F0-4809-B2E7-F9370B129E61"),
                    Name = "SpaceX",
                    Country = "America",
                    Industry="Rocket,Electric Car",
                    Product="Tesla",
                    Introduction = "Rocket,Electric Car"
                },
                new Company
                {
                    Id = Guid.Parse("431C949B-7B83-46AC-943B-D4E773495041"),
                    Name = "MeiTuan",
                    Country = "China",
                    Industry= "Delivery",
                    Product="Mei Tuan App",
                    Introduction = "Take away"
                }) ;

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("07389C3C-AC3E-46F9-B6BD-5320B7EB326D"),
                    CompanyId = Guid.Parse("84C8FF0A-90A7-4F54-B47B-4C933F4F2115"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "cm20210001",
                    FirstName = "Nick",
                    LastName = "Brand",
                    Gender = Gender.男
                }, new Employee
                {
                    Id = Guid.Parse("5E23CC44-50F3-4EF7-976B-92E0991899B8"),
                    CompanyId = Guid.Parse("84C8FF0A-90A7-4F54-B47B-4C933F4F2115"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "cm20210002",
                    FirstName = "Vince",
                    LastName = "John",
                    Gender = Gender.女
                }, new Employee
                {
                    Id=Guid.Parse("A9061730-7868-4869-9F96-F17D5AC97690"),
                    CompanyId = Guid.Parse("84C8FF0A-90A7-4F54-B47B-4C933F4F2115"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "cm20210003",
                    FirstName = "Bryant",
                    LastName = "Kobbo",
                    Gender = Gender.男
                });

        }

    }
}
