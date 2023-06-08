using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop
{
    public class ReceivedStatus
    {
        public int ReceivedStatusId { get; set; }
        public DateTime ArivalDate { get; set; }
        public Condition Condition { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee EmployeeLink { get; set; }

        public int ShipmentId { get; set; }
        public Shipment ShipmentLink { get; set; }
    }

    public enum Condition
    {
        Damaged,
        Good
    }


    public class Shipment
    {
        public int ShipmentId { get; set; }
        public float ShipmentCost { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }

        public bool IsDeleted { get; set; }

        public int ShipperId { get; set; }
        public Shipper ShipperLink { get; set; }

        public int CityId { get; set; }
        public City CityLink { get; set; }

        public int VendorId { get; set; }
        public Vendor VendorLink { get; set; }

        [ForeignKey("ShipmentInsurance")]
        public int ShipmentInsuranceId { get; set; }
        public ShipmentInsurance ShipmentInsuranceLink { get; set; }

        public int BoughtStatusId { get; set; }
        public BoughtStatus BoughtStatusLink { get; set; }

        [ForeignKey("ReceivedStatus")]
        public int ReceivedStatusId { get; set; }
        public ReceivedStatus ReceivedStatusLink { get; set; }
    }

    public class Shipper
    {
        public int ShipperId { get; set; }
        public string ShipperName { get; set; }
        public string ShipperContact { get; set; }
        public string ShipperEmail { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Shipment> ShipmentList { get; set; }
    }

    public class ShipmentInsurance
    {
        public int ShipmentInsuranceId { get; set; }
        public float InsuranceValue { get; set; }
        public DateTime DateInsured { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Shipment> ShipmentList { get; set; }

        public int InsuranceTypeId { get; set; }
        public InsuranceType InsuranceTypeLink { get; set; }

        public int InsurerId { get; set; }
        public Insurer InsurerLink { get; set; }
    }

    public class InsuranceType
    {
        public int InsuranceTypeId { get; set; }
        public string InsuranceName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ShipmentInsurance> ShipmentInsuranceList { get; set; }
    }

    public class Insurer
    {
        public int InsurerId { get; set; }
        public string InsurerName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ShipmentInsurance> ShipmentInsuranceList { get; set; }
    }

    public class Vendor
    {
        public int VendorId { get; set; }
        public bool IsCompany { get; set; }
        public string CompanyLName { get; set; }
        public string CompanyFName { get; set; }
        public string? CompanyName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Shipment> OriginList { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public float ExchangeRate { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Shipment> OriginList { get; set; }
    }
}


