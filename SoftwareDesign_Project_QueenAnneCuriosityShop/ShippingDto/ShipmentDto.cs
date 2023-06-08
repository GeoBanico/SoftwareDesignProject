using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop.Annotations;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto
{
    public class ShipmentDto : INotifyPropertyChanged
    {
        private bool _isCompany;

        //City Class
        public int CityId { get; set; }
        public string CityName { get; set; }
        public float ExchangeRate { get; set; }

        //Vendor Class
        public int VendorId { get; set; }

        public bool IsCompany
        {
            get => _isCompany;
            set
            {
                _isCompany = value;
                OnPropertyChanged(nameof(IsCompany));
            }
        }

        public string CompanyLName { get; set; }
        public string CompanyFName { get; set; }
        public string? CompanyName { get; set; }
        public string CompanyDisplayName { get; set; }

        //Shipper Class
        public int ShipperId { get; set; }
        public string ShipperName { get; set; }
        public string ShipperContact { get; set; }
        public string ShipperEmail { get; set; }

        //Insurer Class
        public int InsurerId { get; set; }
        public string InsurerName { get; set; }

        //Insurance Type Class
        public int InsuranceTypeId { get; set; }
        public string InsuranceName { get; set; }

        //Shipment Class
        public int ShipmentId { get; set; } //List
        public float ShipmentCost { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }

        //Shipment Insurance Class
        public int ShipmentInsuranceId { get; set; }
        public float InsuranceValue { get; set; }
        public DateTime DateInsured { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
