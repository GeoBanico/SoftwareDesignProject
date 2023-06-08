using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto
{
    public class SubShipmentDto
    {
        //Shipper Class
        public string ShipperName { get; set; } //List

        //InsuranceType Class
        public string InsuranceType { get; set; }

        //Insurer Class
        public string InsurerName { get; set; }
        public float InsuranceValue { get; set; }
        public DateTime DateInsured { get; set; }

        //Shipment Class
        public int ShipmentId { get; set; } //List
        public float ShipmentCost { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }

        //Vendor Class
        public string CompanyName { get; set; } //List

        //City Class
        public string CityName { get; set; }

        //Received Class
        public int ReceivedId { get; set; }
    }
}
