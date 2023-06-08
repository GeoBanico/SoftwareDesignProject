using System;
using System.Collections.Generic;
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
using UI_QueenAnnes.Annotations;
using DateTime = System.DateTime;
using Exception = System.Exception;

namespace UI_QueenAnnes.BoughtStatus
{
    /// <summary>
    /// Interaction logic for ViewStoreBought.xaml
    /// </summary>
    public partial class ViewStoreBought : Window
    {
        private ViewStoreBoughtBinding _binding = new ViewStoreBoughtBinding();
        private string StoreId { get; set; } = null;
        private BuyStatus BuyStatusLabel { get; set; }
        private int UnitId { get; set; }

        //For Add
        public ViewStoreBought(BuyStatus bS, int uId)
        {
            InitializeComponent();
            _binding.SetComboBoxFields("","");
            DataContext = _binding;
            BuyStatusLabel = bS;
            UnitId = uId;
        }

        //For Edit
        public ViewStoreBought(int sId, BuyStatus bS, int uId)
        {
            InitializeComponent();
            _binding.FillFieldsEdit(sId);
            DataContext = _binding;
            StoreId = sId.ToString();
            BuyStatusLabel = bS;
            UnitId = uId;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)    
        {
            if (CheckDetails())
            {
                if (string.IsNullOrEmpty(StoreId)) _binding.AddStore(float.Parse(TxtTotal.Text),
                    int.Parse(TxtQuantity.Text), DpDateBought.SelectedDate.Value,
                    CmbCity.SelectedItem.ToString(), CmbStore.SelectedItem.ToString(), BuyStatusLabel, UnitId);
                else _binding.EditStore(float.Parse(TxtTotal.Text), int.Parse(TxtQuantity.Text),
                    DpDateBought.SelectedDate.Value, CmbCity.SelectedItem.ToString(),
                    CmbStore.SelectedItem.ToString(), int.Parse(StoreId), BuyStatusLabel, UnitId);

                Close();
            }
        }

        private bool CheckDetails()
        {
            try
            {
                if(string.IsNullOrEmpty(TxtQuantity.Text)) throw new Exception();
                if(string.IsNullOrEmpty(TxtTotal.Text)) throw new Exception();
                if(DpDateBought.SelectedDate == null) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            try
            {
                var i = int.Parse(TxtQuantity.Text);
                var x = float.Parse(TxtTotal.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please input correct numerical values on Quantity or Total fields");
                return false;
            }

            return true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelMainFunctionality()
        {
            TxtQuantity.IsEnabled = false;
            TxtTotal.IsEnabled = false;
            DpDateBought.IsEnabled = false;
        }

        private void ReturnMainFunctionality()
        {
            TxtQuantity.IsEnabled = true;
            TxtTotal.IsEnabled = true;
            DpDateBought.IsEnabled = true;
        }

        //Store City Funtionality
        private void btnViewCity_Click(object sender, RoutedEventArgs e)
        {
            StoreTabControl.Visibility = Visibility.Visible;
            TabCity.Visibility = Visibility.Visible;
            TabCityDetails.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            btnViewStore.IsEnabled = false;
            TxtStoreCityName.Text = _binding.SelectedCity;
        }

        private void BtnStoreCityAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtStoreCityName.Text = "";
            TxtStoreCityName.IsEnabled = true;
            btnViewCity.IsEnabled = false;

            CityHideButtons();
        }

        private string OldCityName { get; set; } = null;
        private void BtnStoreCityEdit_Click(object sender, RoutedEventArgs e)
        {
            OldCityName = TxtStoreCityName.Text;
            TxtStoreCityName.IsEnabled = true;
            btnViewCity.IsEnabled = false;

            CityHideButtons();
        }

        private void BtnStoreCityDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this city: {_binding.SelectedCity}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.CityDelete(_binding.SelectedCity);
                MessageBox.Show("Delete Successful");
            }
            CityReturnToNormal();
        }

        private void BtnStoreCitySave_Click(object sender, RoutedEventArgs e)
        {
            if(!CityCheckFields()) return;

            if (string.IsNullOrEmpty(OldCityName)) //Add
            {
                _binding.CityAddToSave(TxtStoreCityName.Text);
                CityReturnToNormal();
            }
            else
            {
                _binding.CityEditToSave(OldCityName,TxtStoreCityName.Text);
                CityReturnToNormal();
            }
        }

        private void BtnStoreCityBack_Click(object sender, RoutedEventArgs e)
        {
            CityReturnToNormal();
        }

        private bool CityCheckFields()
        {
            try
            {
                if(string.IsNullOrEmpty(TxtStoreCityName.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill in the empty fields");
                return false;
            }

            return true;
        }

        private void CityReturnToNormal()
        {
            _binding.SetComboBoxFields("",_binding.SelectedStores);
            var holdChanges = new[]
            {
                TxtQuantity.Text, 
                TxtTotal.Text, 
                DpDateBought.SelectedDate.ToString(), 
                CmbStore.SelectedItem.ToString(), ""
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);
            TxtStoreCityName.IsEnabled = false;

            CityReturnButtons();
            ReturnMainFunctionality();
            btnViewStore.IsEnabled = true;

            OldCityName = null;
        }

        private void ReturnChanges(string[] holdChanges)
        {
            TxtQuantity.Text = holdChanges[0];
            TxtTotal.Text = holdChanges[1];

            if (!string.IsNullOrEmpty(holdChanges[2]))
            {
                DpDateBought.SelectedDate = DateTime.Parse(holdChanges[2]);
            }

            //Store is being changed
            if (string.IsNullOrEmpty(holdChanges[3])) CmbCity.SelectedItem = holdChanges[3];

            //City is being changed
            if (string.IsNullOrEmpty(holdChanges[4])) CmbStore.SelectedItem = holdChanges[4];
        }

        private void CityHideButtons()
        {
            BtnStoreCityAdd.Visibility = Visibility.Collapsed;
            BtnStoreCityDelete.Visibility = Visibility.Collapsed;
            BtnStoreCityEdit.Visibility = Visibility.Collapsed;

            BtnStoreCitySave.Visibility = Visibility.Visible;
            CmbStore.IsEnabled = false;
        }

        private void CityReturnButtons()
        {
            BtnStoreCityAdd.Visibility = Visibility.Visible;
            BtnStoreCityDelete.Visibility = Visibility.Visible;
            BtnStoreCityEdit.Visibility = Visibility.Visible;

            BtnStoreCitySave.Visibility = Visibility.Collapsed;

            StoreTabControl.Visibility = Visibility.Collapsed;
            TabCity.Visibility = Visibility.Collapsed;
            TabCityDetails.Visibility = Visibility.Collapsed;

            CmbStore.IsEnabled = true;
            btnViewStore.IsEnabled = true;
            btnViewCity.IsEnabled = true;
        }

        //Store Functionality
        private void btnViewStore_Click(object sender, RoutedEventArgs e)
        {
            StoreTabControl.Visibility = Visibility.Visible;
            TabStore.Visibility = Visibility.Visible;
            TabStoreDetails.Visibility = Visibility.Visible;
            StoreTabControl.SelectedIndex = 1;

            CancelMainFunctionality();
            btnViewCity.IsEnabled = false;
            TxtStoreName.Text = _binding.SelectedStores;
        }

        private void BtnStoreAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtStoreName.Text = "";
            TxtStoreName.IsEnabled = true;
            btnViewStore.IsEnabled = false;

            StoreHideButtons();
        }

        private string OldStoreName { get; set; } = null;
        private void BtnStoreEdit_Click(object sender, RoutedEventArgs e)
        {
            OldStoreName = TxtStoreName.Text;
            TxtStoreName.IsEnabled = true;
            btnViewStore.IsEnabled = false;

            StoreHideButtons();
        }

        private void BtnStoreDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this store: {_binding.SelectedStores}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.DeleteStore(TxtStoreName.Text);
                MessageBox.Show("Delete Successful");
            }

            StoreReturnToNormal();
        }

        private void BtnStoreSave_Click(object sender, RoutedEventArgs e)
        {
            if (!StoreCheckFields()) return;

            if (string.IsNullOrEmpty(OldStoreName)) //Add
            {
                _binding.StoreAddToSave(TxtStoreName.Text);
                StoreReturnToNormal();
            }
            else //Edit
            {
                _binding.StoreEditToSave(OldStoreName, TxtStoreName.Text);
                StoreReturnToNormal();
            }
        }

        private void BtnStoreBack_Click(object sender, RoutedEventArgs e)
        {
            StoreReturnToNormal();
        }

        private bool StoreCheckFields()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtStoreName.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill in the empty fields");
                return false;
            }

            return true;
        }

        private void StoreReturnToNormal()
        {
            _binding.SetComboBoxFields(_binding.SelectedCity, "");
            var holdChanges = new[]
            {
                TxtQuantity.Text, TxtTotal.Text, DpDateBought.SelectedDate.ToString(), "", CmbCity.SelectedItem.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);
            TxtStoreName.IsEnabled = false;

            StoreReturnButtons();

            OldStoreName = null;
        }

        private void StoreHideButtons()
        {
            BtnStoreAdd.Visibility = Visibility.Collapsed;
            BtnStoreDelete.Visibility = Visibility.Collapsed;
            BtnStoreEdit.Visibility = Visibility.Collapsed;

            BtnStoreSave.Visibility = Visibility.Visible;
            CmbCity.IsEnabled = false;
        }

        private void StoreReturnButtons()
        {
            BtnStoreAdd.Visibility = Visibility.Visible;
            BtnStoreDelete.Visibility = Visibility.Visible;
            BtnStoreEdit.Visibility = Visibility.Visible;

            BtnStoreSave.Visibility = Visibility.Collapsed;

            StoreTabControl.Visibility = Visibility.Collapsed;
            TabStore.Visibility = Visibility.Collapsed;
            TabStoreDetails.Visibility = Visibility.Collapsed;

            CmbCity.IsEnabled = true;
            btnViewStore.IsEnabled = true;
            btnViewCity.IsEnabled = true;
        }
    }

    public class ViewStoreBoughtBinding : INotifyPropertyChanged
    {
        private StoreDto _selectedStore;
        private string _selectedStores;
        private string _selectedCity;

        public List<string> CityList { get; set; } = new List<string>();

        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public List<string> StoreList { get; set; } = new List<string>();

        public string SelectedStores
        {
            get => _selectedStores;
            set
            {
                _selectedStores = value;
                OnPropertyChanged(nameof(SelectedStores));
            }
        }

        public StoreDto SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }

        public void SetComboBoxFields(string city, string stores)
        {
            using var context = new CuriosityContext();
            var cityContext = context.StoreCities
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.CityName)
                .ToList();

            var storeContext = context.Stores
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.StoreName)
                .ToList();
            
            CityList.Clear();
            foreach (var storeCity in cityContext)
            {
                CityList.Add(storeCity.CityName);
            }
            if(CityList.Count > 0) SelectedCity = string.IsNullOrEmpty(city) ? CityList[0] : city;

            StoreList.Clear();
            foreach (var store in storeContext)
            {
                StoreList.Add(store.StoreName);
            }
            if(StoreList.Count > 0) SelectedStores = string.IsNullOrEmpty(stores) ? StoreList[0] : stores;
        }

        public void FillFieldsEdit(int sId)
        {
            using var context = new CuriosityContext();
            var storeContext = context.StoreBoughts
                .Include(a => a.StoreCityLink)
                .Include(a => a.StoreLink)
                .First(a => a.StoreBoughtId == sId);

            SelectedStore = new StoreDto()
            {
                CityName = storeContext.StoreCityLink.CityName,
                StoreName = storeContext.StoreLink.StoreName,
                Total = storeContext.Total,
                DateBought = storeContext.DateBought,
                Quantity = storeContext.Quantity,
            };

            SetComboBoxFields(SelectedStore.CityName, SelectedStore.StoreName);
        }

        public void AddStore(float total, int quantity, DateTime date, string city, string store,
            BuyStatus bS, int uId)
        {
            using var context = new CuriosityContext();

            var cityContext = context.StoreCities
                .First(a => a.CityName == city);

            var storeContext = context.Stores
                .First(a => a.StoreName == store);

            var bSContext = context.BoughtStatuses
                .First(a => a.Status == bS);

            var unitContext = context.Units.Find(uId);

            unitContext.UnitQuantity += quantity;

            var newStoreBought = new StoreBought()
            {
                Total = total,
                Quantity = quantity,
                DateBought = date,

                StoreCityLink = cityContext,
                StoreCityId = cityContext.StoreCityId,

                StoreId = storeContext.StoreId,
                StoreLink = storeContext,

                IsDeleted = false,

                BoughtStatusId = bSContext.BoughtStatusId,
                BoughtStatusLink = bSContext
            };

            unitContext.BoughtStatusId = bSContext.BoughtStatusId;
            unitContext.BoughtStatusLink = bSContext;

            context.StoreBoughts.Add(newStoreBought);
            context.SaveChanges();

            MessageBox.Show("Added Successfully... now returning");
        }

        public void EditStore(float total, int quantity, DateTime date, string city, string store, 
            int sId, BuyStatus bS, int uId)
        {
            using var context = new CuriosityContext();

            var myContext = context.StoreBoughts
                .Find(sId);

            var cityContext = context.StoreCities
                .First(a => a.CityName == city);

            var storeContext = context.Stores
                .First(a => a.StoreName == store);

            var bSContext = context.BoughtStatuses
                .First(a => a.Status == bS);

            var unitContext = context.Units
                .Find(uId);

            if (myContext.Quantity - quantity < 0) 
                unitContext.UnitQuantity += Math.Abs(myContext.Quantity - quantity);
            else if(myContext.Quantity - quantity > 0) 
                unitContext.UnitQuantity -= Math.Abs(myContext.Quantity - quantity);

            myContext.Total = total;
            myContext.DateBought = date;
            myContext.Quantity = quantity;

            myContext.StoreCityLink = cityContext;
            myContext.StoreCityId = cityContext.StoreCityId;

            myContext.StoreLink = storeContext;
            myContext.StoreCityId = storeContext.StoreId;

            myContext.BoughtStatusId = bSContext.BoughtStatusId;
            myContext.BoughtStatusLink = bSContext;

            context.SaveChanges();
        }

        //STORE BOUGHT CITY
        public void CityAddToSave(string cityName)
        {
            using var context = new CuriosityContext();

            //Check if there is same name
            try
            {
                var cityContext = context.StoreCities
                    .Where(a => a.CityName == cityName)
                    .ToList();

                if (cityContext.Count > 0) throw new Exception();

                var newCity = new StoreCity()
                {
                    CityName = cityName
                };

                context.StoreCities.Add(newCity);
                context.SaveChanges();

                MessageBox.Show("Add successful... now returning");
            }
            catch (Exception e)
            {
                MessageBox.Show("Inputted City has already been added");
            }
        }

        public void CityEditToSave(string oldCityName, string cityName)
        {
            using var context = new CuriosityContext();
            try
            {
                var cityFindContext = context.StoreCities
                    .Where(a => a.CityName == cityName)
                    .ToList();

                if (cityFindContext.Count > 0) throw new Exception();

                var cityContext = context.StoreCities
                    .First(a => a.CityName == oldCityName);

                cityContext.CityName = cityName;

                context.SaveChanges();
                MessageBox.Show("Edit Successful");
            }
            catch (Exception e)
            {
                MessageBox.Show("Inputted City has already been added");
            }
        }

        public void CityDelete(string storeCity)
        {
            using var context = new CuriosityContext();
            var deleteCity = context.StoreCities
                .First(a => a.CityName == storeCity);

            deleteCity.IsDeleted = true;

            context.SaveChanges();
        }

        //STORE BOUGHT STORE
        public void StoreAddToSave(string name)
        {
            using var context = new CuriosityContext();

            try
            {
                var storeContext = context.Stores
                    .Where(a => a.StoreName == name)
                    .ToList();

                if (storeContext.Count > 0) throw new Exception();

                var newStore = new Store()
                {
                    StoreName = name
                };

                context.Stores.Add(newStore);
                context.SaveChanges();

                MessageBox.Show("Add successful... now returning");
            }
            catch (Exception e)
            {
                MessageBox.Show("Store is already available");
            }
        }

        public void StoreEditToSave(string oldName, string newName)
        {
            using var context = new CuriosityContext();

            try
            {
                var storeFindContext = context.Stores
                    .Where(a => a.StoreName == newName)
                    .ToList();

                if (storeFindContext.Count > 0) throw new Exception();

                var storeContext = context.Stores
                    .First(a => a.StoreName == oldName);

                storeContext.StoreName = newName;
                context.SaveChanges();

                MessageBox.Show("Edit successful");
            }
            catch (Exception e)
            {
                MessageBox.Show("Store is already available");
            }
        }

        public void DeleteStore(string storeName)
        {
            using var context = new CuriosityContext();
            var storeContext = context.Stores
                .First(a => a.StoreName == storeName);

            storeContext.IsDeleted = true;
            context.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
