using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop
{
    public class CuriosityContext : DbContext
    {
        //-- PERSON CLASS--
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        /*
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<AccessType> AccessTypes { get; set; }*/

        //public DbSet<Item> Items { get; set; }
        //public DbSet<SaleItem> SaleItems { get; set; }

        //-- SALE CLASS--
        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<SaleUnit> SaleUnits { get; set; }

        //public DbSet<Insurance> Insurances { get; set; }
        //-- SHIPMENT CLASS --
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ReceivedStatus> ReceivedStatuses { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Insurer> Insurers { get; set; }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<ShipmentInsurance> ShipmentInsurances { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<City> Cities { get; set; }

        //-- BOUGHTSTATUS CLASS --
        public DbSet<BoughtStatus> BoughtStatuses { get; set; }
        public DbSet<StoreBought> StoreBoughts { get; set; }
        public DbSet<StoreCity> StoreCities { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Online> Onlines { get; set; }
        public DbSet<OnlineStore> OnlineStores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-10CNVUQK\\SQLACCESS;Database=CuriosityDatabase;Trusted_Connection=true");

            }
        }

        protected override void OnModelCreating(ModelBuilder mB)
        {
            mB.Entity<Person>()
                .HasOne(a => a.EmployeeLink)
                .WithOne(a => a.PersonLink)
                .HasForeignKey<Employee>(a => a.EmployeeId);

            mB.Entity<Shipment>()
                .HasOne(a => a.ReceivedStatusLink)
                .WithOne(a => a.ShipmentLink)
                .HasForeignKey<ReceivedStatus>(a => a.ReceivedStatusId);

            mB.Entity<Unit>()
                .HasOne(a => a.BoughtStatusLink)
                .WithMany(a => a.UnitList)
                .HasForeignKey(a => a.BoughtStatusId);

            mB.Entity<Person>()
                .HasOne(a => a.PersonLink)
                .WithMany(a => a.PersonList);

        }
    }
}
