using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop.Annotations;
using SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatusDto;
using SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.UnitDto
{
    public class CategoryDto : INotifyPropertyChanged
    {
        private string _categoryName;
        private float _unitPrice;
        private string _unitDescription;
        private int _unitId;
        private string _status;

        //Category Table
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        //Unit Table
        public int UnitId
        {
            get => _unitId;
            set
            {
                _unitId = value;
                OnPropertyChanged(nameof(UnitId));
            }
        }

        public float UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;
                OnPropertyChanged(nameof(UnitPrice));
            }
        }

        public string UnitDescription
        {
            get => _unitDescription;
            set
            {
                _unitDescription = value;
                OnPropertyChanged(nameof(UnitDescription));
            }
        }

        public int UnitQuantity { get; set; }

        //BoughtStatus
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ObservableCollection<SubShipmentDto> ShipmentList { get; } = new ObservableCollection<SubShipmentDto>();
        public ObservableCollection<OnlineDto> OnlineList { get; } = new ObservableCollection<OnlineDto>();
        public ObservableCollection<StoreDto> StoreList { get; } = new ObservableCollection<StoreDto>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
