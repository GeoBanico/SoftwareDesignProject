using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop
{
    public class BoughtStatus
    {
        public int BoughtStatusId { get; set; }
        public BuyStatus Status { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Unit> UnitList { get; set; }
        public ICollection<StoreBought> StoreBoughtList { get; set; }
        public ICollection<Shipment> ShipmentList { get; set; }
        public ICollection<Online> OnlineList { get; set; }
    }

    public enum BuyStatus
    {
        DirectSupplier,
        Online,
        StoreBought
    }

    public class StoreBought
    {
        public int StoreBoughtId { get; set; }
        public float Total { get; set; }
        public int Quantity { get; set; }
        public DateTime DateBought { get; set; }

        public bool IsDeleted { get; set; }

        public int BoughtStatusId { get; set; }
        public BoughtStatus BoughtStatusLink { get; set; }

        public int StoreCityId { get; set; }
        public StoreCity StoreCityLink { get; set; }

        public int StoreId { get; set; }
        public Store StoreLink { get; set; }
    }

    public class StoreCity
    {
        public int StoreCityId { get; set; }
        public string CityName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<StoreBought> StoreBoughtList { get; set; }
    }

    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<StoreBought> StoreBoughtList { get; set; }
    }

    public class Online
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OnlineId { get; set; }
        public DateTime DateBought { get; set; }
        public DateTime ArrivalDate { get; set; }
        public float Total { get; set; }
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public int BoughtStatusId { get; set; }
        public BoughtStatus BoughtStatusLink { get; set; }

        public int OnlineStoreId { get; set; }
        public OnlineStore OnlineStoreLink { get; set; }
    }

    public class OnlineStore
    {
        public int OnlineStoreId { get; set; }
        public string StoreName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Online> OnlineList { get; set; }
    }
}
