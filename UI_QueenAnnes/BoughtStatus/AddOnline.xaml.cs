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

namespace UI_QueenAnnes.BoughtStatus
{
    /// <summary>
    /// Interaction logic for AddOnline.xaml
    /// </summary>
    public partial class AddOnline : Window
    {
        private AddOnlineBinding _binding = new AddOnlineBinding();
        private BuyStatus BuyStatusLabel { get; set; }
        private int UnitId { get; set; }
        public AddOnline(BuyStatus bS, int uId)
        {
            InitializeComponent();
            _binding.FillComboBoxFields("");
            DataContext = _binding;
            BuyStatusLabel = bS;
            UnitId = uId;
        }

       
        private string OnlineId { get; set; } = null;
        public AddOnline(int oId, BuyStatus bS, int uId)
        {
            InitializeComponent();
            _binding.EditFillFields(oId);
            DataContext = _binding;
            OnlineId = oId.ToString();
            BuyStatusLabel = bS;
            UnitId = uId;
        }

        private void btnViewStore_Click(object sender, RoutedEventArgs e)
        {
            GrdOnlineStore.Visibility = Visibility.Visible;
            TxtStoreName.Text = _binding.SelectedStore;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (string.IsNullOrEmpty(OnlineId)) _binding.AddToSave(DateTime.Parse(DpDateBought.SelectedDate.ToString()),
                    DateTime.Parse(DpArrivalDate.SelectedDate.ToString()), float.Parse(TxtTotal.Text),
                    int.Parse(TxtQuantity.Text), CmbStore.SelectedItem.ToString(), BuyStatusLabel, UnitId);
                else
                {
                    _binding.EditToSave(DateTime.Parse(DpDateBought.SelectedDate.ToString()),
                        DateTime.Parse(DpArrivalDate.SelectedDate.ToString()), float.Parse(TxtTotal.Text),
                        int.Parse(TxtQuantity.Text), CmbStore.SelectedItem.ToString(), int.Parse(OnlineId), BuyStatusLabel, UnitId);
                }
                Close();
            }
            
        }

        private bool CheckFields()
        {
            try
            {
                if(string.IsNullOrEmpty(TxtQuantity.Text)) throw new Exception();
                if (string.IsNullOrEmpty(TxtTotal.Text)) throw new Exception();
                if (string.IsNullOrEmpty(DpDateBought.SelectedDate.ToString())) throw new Exception();
                if (string.IsNullOrEmpty(DpArrivalDate.SelectedDate.ToString())) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            try
            {
                int.Parse(TxtQuantity.Text);
                float.Parse(TxtTotal.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter a numerical input");
                return false;
            }

            try
            {
                if(DpDateBought.SelectedDate > DpArrivalDate.SelectedDate) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Departure Date should be less than the Arrival Date" +
                                "\nPlease Change...");
                throw;
            }

            return true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Online Store functionality
        private bool isNewStore { get; set; }
        private void BtnOnlineStoreAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtStoreName.IsEnabled = true;
            TxtStoreName.Text = "";
            btnViewStore.IsEnabled = false;

            isNewStore = true;
            HideAddEditDelete();
        }

        private string OldStoreName { get; set; } = new string("");
        private void BtnOnlineStoreEdit_Click(object sender, RoutedEventArgs e)
        {
            TxtStoreName.IsEnabled = true;
            btnViewStore.IsEnabled = false;

            isNewStore = false;
            OldStoreName = TxtStoreName.Text;
            HideAddEditDelete();
        }

        private void BtnOnlineStoreDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this online store: {_binding.SelectedStore}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.OnlineDelete(_binding.SelectedStore);
                MessageBox.Show("Online Store Deleted");
            }
            ReturnToNormal();
        }

        private void BtnOnlineStoreSave_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckOnlineStoreFields(TxtStoreName.Text)) return;

            if (isNewStore) //Add
            {
                _binding.AddOnlineStoreToSave(TxtStoreName.Text);
                ReturnToNormal();
            }
            else
            {
                _binding.EditOnlineStoreToSave(OldStoreName, TxtStoreName.Text);
                ReturnToNormal();
            }
        }

        private void BtnOnlineStoreBack_Click(object sender, RoutedEventArgs e)
        {
            ReturnToNormal();
        }

        private void ReturnToNormal()
        {
            _binding.FillComboBoxFields("");
            var holdChanges = new [] { TxtQuantity.Text, TxtTotal.Text, DpDateBought.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString()};

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);
            TxtStoreName.IsEnabled = false;

            ShowAddEditDelete();
        }

        private void ReturnChanges(string[] holdChanges)
        {
            TxtQuantity.Text = holdChanges[0];
            TxtTotal.Text = holdChanges[1];

            if (!string.IsNullOrEmpty(holdChanges[2]))
            {
                DpDateBought.SelectedDate = DateTime.Parse(holdChanges[2]);
            }

            if (!string.IsNullOrEmpty(holdChanges[3]))
            {
                DpArrivalDate.SelectedDate = DateTime.Parse(holdChanges[3]);
            }
        }

        private bool CheckOnlineStoreFields(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            return true;
        }

        private void HideAddEditDelete()
        {
            BtnOnlineStoreAdd.Visibility = Visibility.Collapsed;
            BtnOnlineStoreEdit.Visibility = Visibility.Collapsed;
            BtnOnlineStoreDelete.Visibility = Visibility.Collapsed;

            BtnOnlineStoreSave.Visibility = Visibility.Visible;
        }

        private void ShowAddEditDelete()
        {
            BtnOnlineStoreAdd.Visibility = Visibility.Visible;
            BtnOnlineStoreEdit.Visibility = Visibility.Visible;
            BtnOnlineStoreDelete.Visibility = Visibility.Visible;

            BtnOnlineStoreSave.Visibility = Visibility.Collapsed;

            GrdOnlineStore.Visibility = Visibility.Collapsed;
            btnViewStore.IsEnabled = true;
        }
    }

    public class AddOnlineBinding : INotifyPropertyChanged
    {
        private OnlineDto _selectedOnline;
        private string _selectedStore;

        public OnlineDto SelectedOnline
        {
            get => _selectedOnline;
            set
            {
                _selectedOnline = value;
                OnPropertyChanged(nameof(SelectedOnline));
            }
        }

        public List<string> StoreList { get; set; } = new List<string>();
        public string SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }

        public void FillComboBoxFields(string store)
        {
            using var context = new CuriosityContext();
            var onlineContext = context.OnlineStores
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.StoreName)
                .ToList();

            StoreList.Clear();
            foreach (var onlineStore in onlineContext)
            {
                StoreList.Add(onlineStore.StoreName);
            }

            if(StoreList.Count > 0) SelectedStore = string.IsNullOrEmpty(store) ? StoreList[0] : store;
        }

        public void EditFillFields(int oId)
        {
            using var context = new CuriosityContext();
            var onlineContext = context.Onlines
                .Include(a => a.OnlineStoreLink)
                .First(a => a.OnlineId == oId);

            SelectedOnline = new OnlineDto()
            {
                Total = onlineContext.Total,
                Quantity = onlineContext.Quantity,
                DateBought = onlineContext.DateBought,
                ArrivalDate = onlineContext.ArrivalDate,
                StoreName = onlineContext.OnlineStoreLink.StoreName
            };

            FillComboBoxFields(SelectedOnline.StoreName);
        }

        public void AddToSave(DateTime dateBought, DateTime arrDate, float total, int quantity, 
            string store, BuyStatus bS, int uId)
        {
            using var context = new CuriosityContext();

            var storeContext = context.OnlineStores
                .First(a => a.StoreName == store);

            try
            {
                var Context = context.BoughtStatuses
                    .First(a => a.Status == bS);
            }
            catch (Exception e)
            {
                var fill = new FillBoughtStatus();
                fill.FillValues();

            }

            var bSContext = context.BoughtStatuses
                .First(a => a.Status == bS);

            var unitContext = context.Units
                .Find(uId);

            var newOnline = new Online()
            {
                DateBought = dateBought,
                ArrivalDate = arrDate,
                Total = total,
                Quantity = quantity,

                IsDeleted = false,

                OnlineStoreLink = storeContext,
                OnlineStoreId = storeContext.OnlineStoreId,

                BoughtStatusLink = bSContext,
                BoughtStatusId = bSContext.BoughtStatusId
            };

            unitContext.UnitQuantity += quantity;

            unitContext.BoughtStatusId = bSContext.BoughtStatusId;
            unitContext.BoughtStatusLink = bSContext;

            context.Onlines.Add(newOnline);
            context.SaveChanges();
            MessageBox.Show("Add successful... now returning");
        }

        public void EditToSave(DateTime dateBought, DateTime arrDate, float total, int quantity, 
            string store, int oId, BuyStatus bS, int uId)
        {
            using var context = new CuriosityContext();

            var storeContext = context.OnlineStores
                .First(a => a.StoreName == store);

            var onlineToEdit = context.Onlines
                .Find(oId);

            try
            {
                var Context = context.BoughtStatuses
                    .First(a => a.Status == bS);
            }
            catch (Exception e)
            {
                var fill = new FillBoughtStatus();
                fill.FillValues();

            }

            var bSContext = context.BoughtStatuses
                .First(a => a.Status == bS);

            var unitContext = context.Units
                .Find(uId);

            if(onlineToEdit.Quantity - quantity < 0) unitContext.UnitQuantity += Math.Abs(onlineToEdit.Quantity - quantity);
            else if (onlineToEdit.Quantity - quantity > 0)
                unitContext.UnitQuantity -= Math.Abs(onlineToEdit.Quantity - quantity);

            onlineToEdit.DateBought = dateBought;
            onlineToEdit.ArrivalDate = arrDate;
            onlineToEdit.Total = total;
            onlineToEdit.Quantity = quantity;

            onlineToEdit.OnlineStoreId = storeContext.OnlineStoreId;
            onlineToEdit.OnlineStoreLink = storeContext;

            //onlineToEdit.BoughtStatusId = bSContext.BoughtStatusId;
            //onlineToEdit.BoughtStatusLink = bSContext;

            context.SaveChanges();
            MessageBox.Show("Edit successful... now returning");
        }

        //Online Store
        public void AddOnlineStoreToSave(string name)
        {
            using var context = new CuriosityContext();
            var sameName = context.OnlineStores
                .Where(a => a.StoreName == name)
                .ToList();

            if (sameName.Count > 0) MessageBox.Show("This online store is already added");
            else
            {
                var newStore = new OnlineStore()
                {
                    StoreName = name,
                    IsDeleted = false
                };

                context.OnlineStores.Add(newStore);
                context.SaveChanges();

                MessageBox.Show("Added successfully");
            }
        }

        public void EditOnlineStoreToSave(string oldName, string newName)
        {
            using var context = new CuriosityContext();

            try
            {
                var sameName = context.OnlineStores
                    .Where(a => a.StoreName == newName)
                    .ToList();

                if (sameName.Count > 0) throw new Exception();

                var storeToEdit = context.OnlineStores
                    .First(a => a.StoreName == oldName);

                storeToEdit.StoreName = newName;

                context.SaveChanges();
                MessageBox.Show("Edit successful");
            }
            catch (Exception e)
            {
                MessageBox.Show("This online store is already added");
            }
        }

        public void OnlineDelete(string storeName)
        {
            using var context = new CuriosityContext();
            var deletedStore = context.OnlineStores
                .First(a => a.StoreName == storeName);

            deletedStore.IsDeleted = true;
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
