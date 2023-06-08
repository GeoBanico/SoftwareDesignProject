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
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto;
using UI_QueenAnnes.Annotations;

namespace UI_QueenAnnes.Person
{
    /// <summary>
    /// Interaction logic for ViewAllDeleted.xaml
    /// </summary>
    public partial class ViewAllDeleted : Window
    {
        private ViewAllDeletedBinding _binding = new ViewAllDeletedBinding();
        public ViewAllDeleted()
        {
            InitializeComponent();
            _binding.AllRefresh();
            DataContext = _binding;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPersonRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestorePerson(_binding.GetPerson().PersonId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnUnitRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreUnit(_binding.GetUnit().UnitId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnCategoryRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreCategory(_binding.GetCategory().CategoryId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnVendorRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreVendor(_binding.GetVendor().VendorId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnShipCityRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreShipCity(_binding.GetShipCity().CityId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnShipperRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreShipper(_binding.GetShipper().ShipperId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnTypeRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreType(_binding.GetType().InsuranceTypeId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnInsurerRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreInsurer(_binding.GetInsurer().InsurerId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnOnlineRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreOnlineStore(_binding.GetOnlineStore().OnlineStoreId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnStoreCityRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreStoreCity(_binding.GetStoreCity().StoreCityId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnStoreRestore_Click(object sender, RoutedEventArgs e)
        {
            _binding.RestoreStore(_binding.GetStore().StoreId);
            DataContext = null;
            DataContext = _binding;
        }
    }

    public class ViewAllDeletedBinding : INotifyPropertyChanged
    {
        public void AllRefresh()
        {
            //- Person -
            PersonListRefresh();
            //- Unit -
            UnitListRefresh();
            CategoryListRefresh();

            //- Item Details -
            //Shipment
            VendorListRefresh();
            ShipCityListRefresh();
            ShipperListRefresh();
            TypeListRefresh();
            InsurerListRefresh();

            //Online
            OnlineStoreListRefresh();

            //StoreBought
            StoreCityListRefresh();

        }

        //PERSON
        private SoftwareDesign_Project_QueenAnneCuriosityShop.Person _selectedPerson;
        public SoftwareDesign_Project_QueenAnneCuriosityShop.Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
        public ObservableCollection<SoftwareDesign_Project_QueenAnneCuriosityShop.Person> PersonList { get; } = new ObservableCollection<SoftwareDesign_Project_QueenAnneCuriosityShop.Person>();

        public void PersonListRefresh()
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Where(a => a.IsDeleted == true)
                .ToList();

            PersonList.Clear();
            foreach (var person in personContext)
            {
                
                var newPerson = new SoftwareDesign_Project_QueenAnneCuriosityShop.Person()
                {
                    PersonId = person.PersonId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    AccessType = person.AccessType,
                };
                PersonList.Add(newPerson);
            }
        }

        public SoftwareDesign_Project_QueenAnneCuriosityShop.Person GetPerson()
        {
            return _selectedPerson;
        }

        public void RestorePerson(int personId)
        {
            using var context = new CuriosityContext();
            var personToRestore = context.Persons.Find(personId);

            personToRestore.IsDeleted = false;
            context.SaveChanges();

            PersonListRefresh();
            MessageBox.Show($"{personToRestore.FirstName} {personToRestore.LastName} has been restored");
        }

        //ITEM-UNIT
        private Unit _selectedUnit;
        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }
        public ObservableCollection<Unit> UnitList { get; } = new ObservableCollection<Unit>();

        public void UnitListRefresh()
        {
            using var context = new CuriosityContext();
            var unitContext = context.Units
                .Where(a => a.IsDeleted == true)
                .ToList();

            UnitList.Clear();
            foreach (var unit in unitContext)
            {
                var newUnit = new Unit()
                {
                    UnitId = unit.UnitId,
                    UnitPrice = unit.UnitPrice,
                    UnitDescription = unit.UnitDescription
                };
                UnitList.Add(newUnit);
            }
        }

        public Unit GetUnit()
        {
            return _selectedUnit;
        }

        public void RestoreUnit(int UnitId)
        {
            using var context = new CuriosityContext();
            var unitToRestore = context.Units.Find(UnitId);

            unitToRestore.IsDeleted = false;
            context.SaveChanges();

            UnitListRefresh();
            MessageBox.Show($"Unit has been restored");
        }

        //ITEM-CATEGORY
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<Category> CategoryList { get; } = new ObservableCollection<Category>();

        public void CategoryListRefresh()
        {
            using var context = new CuriosityContext();
            var categoryContext = context.Categories
                .Where(a => a.IsDeleted == true)
                .ToList();
            CategoryList.Clear();
            foreach (var category in categoryContext)
            {
                var newCategory = new Category()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                };
                CategoryList.Add(newCategory);
            }
        }

        public Category GetCategory()
        {
            return _selectedCategory;
        }

        public void RestoreCategory(int CategoryId)
        {
            using var context = new CuriosityContext();
            var CategoryToRestore = context.Categories.Find(CategoryId);

            CategoryToRestore.IsDeleted = false;
            context.SaveChanges();

            CategoryListRefresh();
            MessageBox.Show($"{CategoryToRestore.CategoryName} has been restored");
        }

        //ITEM DETAILS - DIRECT SUPPLIER - VENDOR
        private ShipmentDto _selectedVendor;
        public ShipmentDto SelectedVendor
        {
            get => _selectedVendor;
            set
            {
                _selectedVendor = value;
                OnPropertyChanged(nameof(SelectedVendor));
            }
        }
        public ObservableCollection<ShipmentDto> VendorList { get; } = new ObservableCollection<ShipmentDto>();

        public void VendorListRefresh()
        {
            using var context = new CuriosityContext();
            var vendorContext = context.Vendors
                .Where(a => a.IsDeleted == true)
                .ToList();

            VendorList.Clear();
            foreach (var vendor in vendorContext)
            {
                
                var newVendor = new ShipmentDto()
                {
                    VendorId = vendor.VendorId,
                };
                newVendor.CompanyDisplayName = vendor.IsCompany ? vendor.CompanyName : $"{vendor.CompanyFName} {vendor.CompanyLName}";
                VendorList.Add(newVendor);
            }
        }

        public ShipmentDto GetVendor()
        {
            return _selectedVendor;
        }

        public void RestoreVendor(int VendorId)
        {
            using var context = new CuriosityContext();
            var vendorToRestore = context.Vendors.Find(VendorId);

            vendorToRestore.IsDeleted = false;
            context.SaveChanges();

            VendorListRefresh();
            MessageBox.Show($"Supplier has been restored");
        }

        //ITEM DETAILS - DIRECT SUPPLIER - CITY
        private ShipmentDto _selectedShipCity;
        public ShipmentDto SelectedShipCity
        {
            get => _selectedShipCity;
            set
            {
                _selectedShipCity = value;
                OnPropertyChanged(nameof(SelectedShipCity));
            }
        }
        public ObservableCollection<ShipmentDto> ShipCityList { get; } = new ObservableCollection<ShipmentDto>();

        public void ShipCityListRefresh()
        {
            using var context = new CuriosityContext();
            var shipCityContext = context.Cities
                .Where(a => a.IsDeleted == true)
                .ToList();

            ShipCityList.Clear();
            foreach (var shipCity in shipCityContext)
            {
                
                var newShipCity = new ShipmentDto()
                {
                    CityName = shipCity.CityName,
                    CityId = shipCity.CityId
                };
                ShipCityList.Add(newShipCity);
            }
        }

        public ShipmentDto GetShipCity()
        {
            return _selectedShipCity;
        }

        public void RestoreShipCity(int ShipCityId)
        {
            using var context = new CuriosityContext();
            var shipCityToRestore = context.Cities.Find(ShipCityId);

            shipCityToRestore.IsDeleted = false;
            context.SaveChanges();

            ShipCityListRefresh();
            MessageBox.Show($"{shipCityToRestore.CityName} has been restored");
        }

        //ITEM DETAILS - DIRECT SUPPLIER - SHIPPER
        private ShipmentDto _selectedShipper;
        public ShipmentDto SelectedShipper
        {
            get => _selectedShipper;
            set
            {
                _selectedShipper = value;
                OnPropertyChanged(nameof(SelectedShipper));
            }
        }
        public ObservableCollection<ShipmentDto> ShipperList { get; } = new ObservableCollection<ShipmentDto>();

        public void ShipperListRefresh()
        {
            using var context = new CuriosityContext();
            var shipperContext = context.Shippers
                .Where(a => a.IsDeleted == true)
                .ToList();

            ShipperList.Clear();
            foreach (var shipper in shipperContext)
            {

                var newShipper = new ShipmentDto()
                {
                    ShipperName = shipper.ShipperName,
                    ShipperId = shipper.ShipperId
                };
                ShipperList.Add(newShipper);
            }
        }

        public ShipmentDto GetShipper()
        {
            return _selectedShipper;
        }

        public void RestoreShipper(int ShipperId)
        {
            using var context = new CuriosityContext();
            var shipperToRestore = context.Shippers.Find(ShipperId);

            shipperToRestore.IsDeleted = false;
            context.SaveChanges();

            ShipperListRefresh();
            MessageBox.Show($"{shipperToRestore.ShipperName} has been restored");
        }

        //ITEM DETAILS - DIRECT SUPPLIER - INSURANCE TYPE
        private ShipmentDto _selectedType;
        public ShipmentDto SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public ObservableCollection<ShipmentDto> TypeList { get; } = new ObservableCollection<ShipmentDto>();

        public void TypeListRefresh()
        {
            using var context = new CuriosityContext();
            var typeContext = context.InsuranceTypes
                .Where(a => a.IsDeleted == true)
                .ToList();

            TypeList.Clear();
            foreach (var type in typeContext)
            {

                var newType = new ShipmentDto()
                {
                    InsuranceName = type.InsuranceName,
                    InsuranceTypeId = type.InsuranceTypeId
                };
                TypeList.Add(newType);
            }
        }

        public ShipmentDto GetType()
        {
            return _selectedType;
        }

        public void RestoreType(int TypeId)
        {
            using var context = new CuriosityContext();
            var typeToRestore = context.InsuranceTypes.Find(TypeId);

            typeToRestore.IsDeleted = false;
            context.SaveChanges();

            TypeListRefresh();
            MessageBox.Show($"{typeToRestore.InsuranceName} has been restored");
        }

        //ITEM DETIALS - DIRECT SUPPLIER - INSURER
        private ShipmentDto _selectedInsurer;
        public ShipmentDto SelectedInsurer
        {
            get => _selectedInsurer;
            set
            {
                _selectedInsurer = value;
                OnPropertyChanged(nameof(SelectedInsurer));
            }
        }
        public ObservableCollection<ShipmentDto> InsurerList { get; } = new ObservableCollection<ShipmentDto>();

        public void InsurerListRefresh()
        {
            using var context = new CuriosityContext();
            var insurerContext = context.Insurers
                .Where(a => a.IsDeleted == true)
                .ToList();

            InsurerList.Clear();
            foreach (var insurer in insurerContext)
            {

                var newInsurer = new ShipmentDto()
                {
                    InsurerName = insurer.InsurerName,
                    InsurerId = insurer.InsurerId
                };
                InsurerList.Add(newInsurer);
            }
        }

        public ShipmentDto GetInsurer()
        {
            return _selectedInsurer;
        }

        public void RestoreInsurer(int InsurerId)
        {
            using var context = new CuriosityContext();
            var insurerToRestore = context.Insurers.Find(InsurerId);

            insurerToRestore.IsDeleted = false;
            context.SaveChanges();

            InsurerListRefresh();
            MessageBox.Show($"{insurerToRestore.InsurerName} has been restored");
        }

        //ITEM DETAILS - ONLINE - CITY
        private OnlineStore _selectedOnlineStore;
        public OnlineStore SelectedOnlineStore
        {
            get => _selectedOnlineStore;
            set
            {
                _selectedOnlineStore = value;
                OnPropertyChanged(nameof(SelectedOnlineStore));
            }
        }
        public ObservableCollection<OnlineStore> OnlineStoreList { get; } = new ObservableCollection<OnlineStore>();

        public void OnlineStoreListRefresh()
        {
            using var context = new CuriosityContext();
            var storeContext = context.OnlineStores
                .Where(a => a.IsDeleted == true)
                .ToList();

            OnlineStoreList.Clear();
            foreach (var store in storeContext)
            {

                var newStore = new OnlineStore()
                {
                    StoreName = store.StoreName,
                    OnlineStoreId = store.OnlineStoreId
                };
                OnlineStoreList.Add(newStore);
            }
        }

        public OnlineStore GetOnlineStore()
        {
            return _selectedOnlineStore;
        }

        public void RestoreOnlineStore(int OnlineStoreId)
        {
            using var context = new CuriosityContext();
            var storeToRestore = context.OnlineStores.Find(OnlineStoreId);

            storeToRestore.IsDeleted = false;
            context.SaveChanges();

            OnlineStoreListRefresh();
            MessageBox.Show($"{storeToRestore.StoreName} has been restored");
        }

        //ITEM DETAILS - STORE BOUGHT - CITY
        private StoreCity _selectedStoreCity;
        public StoreCity SelectedStoreCity
        {
            get => _selectedStoreCity;
            set
            {
                _selectedStoreCity = value;
                OnPropertyChanged(nameof(SelectedStoreCity));
            }
        }
        public ObservableCollection<StoreCity> StoreCityList { get; } = new ObservableCollection<StoreCity>();

        public void StoreCityListRefresh()
        {
            using var context = new CuriosityContext();
            var storeContext = context.StoreCities
                .Where(a => a.IsDeleted == true)
                .ToList();

            StoreCityList.Clear();
            foreach (var store in storeContext)
            {

                var newStore = new StoreCity()
                {
                    CityName = store.CityName,
                    StoreCityId = store.StoreCityId
                };
                StoreCityList.Add(newStore);
            }
        }

        public StoreCity GetStoreCity()
        {
            return _selectedStoreCity;
        }

        public void RestoreStoreCity(int StoreCityId)
        {
            using var context = new CuriosityContext();
            var storeToRestore = context.StoreCities.Find(StoreCityId);

            storeToRestore.IsDeleted = false;
            context.SaveChanges();

            StoreCityListRefresh();
            MessageBox.Show($"{storeToRestore.CityName} has been restored");
        }

        //ITEM DETAILS - STORE BOUGHT - STORE
        private Store _selectedStore;
        public Store SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }
        public ObservableCollection<Store> StoreList { get; } = new ObservableCollection<Store>();

        public void StoreListRefresh()
        {
            using var context = new CuriosityContext();
            var storeContext = context.Stores
                .Where(a => a.IsDeleted == true)
                .ToList();

            StoreList.Clear();
            foreach (var store in storeContext)
            {

                var newStore = new Store()
                {
                    StoreName = store.StoreName,
                    StoreId = store.StoreId
                };
                StoreList.Add(newStore);
            }
        }

        public Store GetStore()
        {
            return _selectedStore;
        }

        public void RestoreStore(int StoreId)
        {
            using var context = new CuriosityContext();
            var storeToRestore = context.Stores.Find(StoreId);

            storeToRestore.IsDeleted = false;
            context.SaveChanges();

            StoreListRefresh();
            MessageBox.Show($"{storeToRestore.StoreName} has been restored");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
