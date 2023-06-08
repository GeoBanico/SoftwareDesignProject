using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Unit> UnitLink { get; set; }

    }

    public class Unit
    {
        public int UnitId { get; set; }
        public float UnitPrice { get; set; }
        public string UnitDescription { get; set; }
        public int UnitQuantity { get; set; }
        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public Category CategoryLink { get; set; }

        public int? BoughtStatusId { get; set; }
        public BoughtStatus BoughtStatusLink { get; set; }

        public ICollection<SaleUnit> SaleUnitList { get; set; }
    }

    public class SaleUnit
    {
        public int SaleUnitId { get; set; }
        public int SaleQuantity { get; set; }
        public DateTime SaleDate { get; set; }
        public float Total { get; set; }
        public bool IsDeleted { get; set; }

        public int UnitId { get; set; }
        public Unit UnitLink { get; set; }

        public int? PersonId { get; set; }
        public Person PersonLink { get; set; }
    }
}
