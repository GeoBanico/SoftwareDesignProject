using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatusDto;
using SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto;
using SoftwareDesign_Project_QueenAnneCuriosityShop.UnitDto;
using UI_QueenAnnes.Annotations;
using UI_QueenAnnes.BoughtStatus;
using UI_QueenAnnes.Shipment;

namespace UI_QueenAnnes.Item
{
    /// <summary>
    /// Interaction logic for AllItemPage.xaml
    /// </summary>
    public partial class AllItemPage : Window
    {
        private AllItemPageBinding _binding = new AllItemPageBinding();
        private int PersonId { get; set; }
        public int UnitId { get; set; }

        public AllItemPage(int pId, int uId)
        {
            InitializeComponent();

            _binding.FillFields(pId, uId);
            DataContext = _binding;

            UnitId = uId;
            PersonId = pId;
        }

        private void BtnUnitAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (CmbAddItemDetails.SelectedItem.ToString())
            {
                case "DirectSupplier":
                {
                    var window = new AddShipment(BuyStatus.DirectSupplier, UnitId);

                    var dialogResult = (bool)window.ShowDialog();
                    if (!dialogResult)
                    {
                        _binding.Refresh(UnitId);
                        ShowFields();
                        DataContext = null;
                        DataContext = _binding;
                    }

                    break;
                }
                case "Online":
                {
                    var window = new AddOnline(BuyStatus.Online, UnitId);

                    var dialogResult = (bool)window.ShowDialog();
                    if (!dialogResult)
                    {
                        _binding.Refresh(UnitId);
                        ShowFields();
                        DataContext = null;
                        DataContext = _binding;
                    }

                    break;
                }
                case "StoreBought":
                {
                    var window = new ViewStoreBought(BuyStatus.StoreBought, UnitId);

                    var dialogResult = (bool) window.ShowDialog();
                    if (!dialogResult)
                    {
                        _binding.Refresh(UnitId);
                        ShowFields();
                        DataContext = null;
                        DataContext = _binding;
                    }

                    break;
                }
            }
        }

        private void BtnStoreAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewStoreBought(BuyStatus.StoreBought, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnStoreEdit_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewStoreBought(_binding.GetStore().StoreBoughtId,BuyStatus.StoreBought, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowFields();
        }

        private void ShowFields()
        {
            
            if (_binding.SelectedItem.Status == "DirectSupplier")
            {
                TabControl.Visibility = Visibility.Visible;
                GrdDS.Visibility = Visibility.Visible;
                TabItemDS.Visibility = Visibility.Visible;
            }
            else if (_binding.SelectedItem.Status == "Online")
            {
                TabControl.Visibility = Visibility.Visible;
                GrdOnline.Visibility = Visibility.Visible;
                TabItemOnline.Visibility = Visibility.Visible;
                TabControl.SelectedIndex = 1;
            }
            else if (_binding.SelectedItem.Status == "StoreBought")
            {
                TabControl.Visibility = Visibility.Visible;
                GrdStore.Visibility = Visibility.Visible;
                TabItemStore.Visibility = Visibility.Visible;
                TabControl.SelectedIndex = 2;
            }
            else
            {
                GrdAddStatus.Visibility = Visibility.Visible;
            }
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainItemPage(PersonId);
            window.Show();
            Close();
        }

        private void BtnReceive_Click(object sender, RoutedEventArgs e)
        {
            var window = new ShipmentReceived(UnitId, _binding.GetShipment().ShipmentId);
            var dialogResult = (bool) window.ShowDialog();
            if (!dialogResult)
            {
                _binding.FillFields(PersonId, UnitId);

                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnDSAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddShipment(BuyStatus.DirectSupplier, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnDSEdit_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddShipment(_binding.GetShipment().ShipmentId, BuyStatus.DirectSupplier, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnOnlineAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddOnline(BuyStatus.Online, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnOnlineEdit_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddOnline(_binding.GetOnline().OnlineId, BuyStatus.Online, UnitId);

            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.Refresh(UnitId);
                ShowFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void LstDirectSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnDSEdit.Visibility = Visibility.Visible;
            BtnReceive.Visibility = Visibility.Visible;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnOnlineEdit.Visibility = Visibility.Visible;
        }

        private void LstStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnStoreEdit.Visibility = Visibility.Visible;
        }
    }

    public class AllItemPageBinding : INotifyPropertyChanged
    {
        public string[] UnitStatus { get; set; } = new[]
            {BuyStatus.DirectSupplier.ToString(), BuyStatus.Online.ToString(), BuyStatus.StoreBought.ToString()};

        public string SelectedStatus { get; set; } = BuyStatus.DirectSupplier.ToString();

        public string Name { get; set; }
        public string AccessType { get; set; }

        private CategoryDto _selectedItem;
        public CategoryDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private SubShipmentDto _selectedShipment;
        public SubShipmentDto SelectedShipment
        {
            get => _selectedShipment;
            set
            {
                _selectedShipment = value;
                OnPropertyChanged(nameof(SelectedShipment));
            }
        }

        public SubShipmentDto GetShipment()
        {
            return _selectedShipment;
        }

        private OnlineDto _selectedOnline;
        public OnlineDto SelectedOnline
        {
            get => _selectedOnline;
            set
            {
                _selectedOnline = value;
                OnPropertyChanged(nameof(SelectedOnline));
            }
        }
        public OnlineDto GetOnline()
        {
            return _selectedOnline;
        }

        private StoreDto _selectedStore;
        public StoreDto SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }
        public StoreDto GetStore()
        {
            return _selectedStore;
        }

        public void FillFields(int pId, int itemId)
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Find(pId);

            Name = $"{personContext.FirstName} {personContext.LastName}";
            AccessType = personContext.AccessType.ToString();

           Refresh(itemId);
        }

        public void Refresh(int itemId)
        {
            using var context = new CuriosityContext();

            //Search Items
            var unit = context.Units
                .Include(a => a.CategoryLink)
                .Include(a => a.BoughtStatusLink)
                .First(a => a.UnitId == itemId);

            SelectedItem = new CategoryDto()
            {
                CategoryName = unit.CategoryLink.CategoryName,
                UnitPrice = unit.UnitPrice,
                UnitDescription = unit.UnitDescription,
                UnitQuantity = unit.UnitQuantity,
                UnitId = unit.UnitId
            };

            if (unit.BoughtStatusId != null) SelectedItem.Status = unit.BoughtStatusLink.Status.ToString();

            switch (SelectedItem.Status)
            {
                case "DirectSupplier":
                    DirectSupplierRefresh(context);
                    break;
                case "Online":
                    OnlineRefresh(context);
                    break;
                case "StoreBought":
                    StoreRefresh(context);
                    break;
                default:
                    SelectedItem.Status = "Status unavailable";
                    break;
            }
        }

        private void DirectSupplierRefresh(CuriosityContext context)
        {
            var dSContext = context.Shipments
                .Include(a => a.CityLink) //City
                .Include(a => a.VendorLink) //Vendor
                .Include(a => a.ShipperLink) //Vendor
                .Include(a => a.ReceivedStatusLink) //Receive
                .Include(a => a.ShipmentInsuranceLink) //Insurance
                .ThenInclude(a => a.InsuranceTypeLink) //InsuranceType
                .Include(a => a.ShipmentInsuranceLink) //Back to Insurance
                .ThenInclude(a => a.InsurerLink) //Insurer
                .OrderBy(a => a.ShipmentId)
                .ToList();

            foreach (var shipment in dSContext)
            {
                var companyName = shipment.VendorLink.IsCompany ? shipment.VendorLink.CompanyName : $"{shipment.VendorLink.CompanyFName} {shipment.VendorLink.CompanyLName}";

                var newShipment = new  SubShipmentDto()
                {
                    ShipperName = shipment.ShipperLink.ShipperName,
                    InsuranceType = shipment.ShipmentInsuranceLink.InsuranceTypeLink.InsuranceName,
                    CityName = shipment.CityLink.CityName,
                    CompanyName = companyName,

                    ShipmentId = shipment.ShipmentId,
                    ShipmentCost = shipment.ShipmentCost,
                    DepartureDate = shipment.DepartureDate,
                    Quantity = shipment.Quantity,
                    ExpectedArrivalDate = shipment.ExpectedArrivalDate,

                    ReceivedId = shipment.ReceivedStatusId
                };
                SelectedItem.ShipmentList.Add(newShipment);
            }
        }

        private void OnlineRefresh(CuriosityContext context)
        {
            var onlineContext = context.Onlines
                .Include(a => a.OnlineStoreLink)
                .OrderBy(a => a.OnlineStoreLink.StoreName)
                .ToList();

            foreach (var online in onlineContext)
            {
                var newOnline = new OnlineDto()
                {
                    OnlineId = online.OnlineId,
                    DateBought = online.DateBought,
                    ArrivalDate = online.ArrivalDate,
                    Total = online.Total,
                    Quantity = online.Quantity,

                    StoreName = online.OnlineStoreLink.StoreName
                };

                SelectedItem.OnlineList.Add(newOnline);
            }
        }

        private void StoreRefresh(CuriosityContext context)
        {
            var storeContext = context.StoreBoughts
                .Include(a => a.StoreCityLink)
                .Include(a => a.StoreLink)
                .OrderBy(a => a.StoreBoughtId)
                .ToList();

            foreach (var storeBought in storeContext)
            {
                var newStore = new StoreDto()
                {
                    DateBought = storeBought.DateBought,
                    Quantity = storeBought.Quantity,
                    StoreBoughtId = storeBought.StoreBoughtId,
                    Total = storeBought.Total,

                    CityName = storeBought.StoreCityLink.CityName,

                    StoreName = storeBought.StoreLink.StoreName
                };

                SelectedItem.StoreList.Add(newStore);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
