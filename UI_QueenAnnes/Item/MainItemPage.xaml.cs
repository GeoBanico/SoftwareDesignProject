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
using Microsoft.VisualBasic;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto;
using SoftwareDesign_Project_QueenAnneCuriosityShop.UnitDto;
using UI_QueenAnnes.Annotations;
using UI_QueenAnnes.Person;

namespace UI_QueenAnnes.Item
{
    /// <summary>
    /// Interaction logic for MainItemPage.xaml
    /// </summary>
    public partial class MainItemPage : Window
    {
        private int PersonID { get; set; }
        private MainItemPageBinding _binding = new MainItemPageBinding();
        public MainItemPage(int pID)
        {
            InitializeComponent();
            _binding.FillFields(pID);
            PersonID = pID;
            DataContext = _binding;
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            GrdBuy.Visibility = Visibility.Visible;
            BtnBuy.Visibility = Visibility.Collapsed;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LblSearch.Visibility = !string.IsNullOrEmpty(TxtSearch.Text) ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnAllItems_Click(object sender, RoutedEventArgs e)
        {
            var viewAllItem = new AllItemPage(PersonID, _binding.GetSelectedUnit().UnitId);
            viewAllItem.Show();

            Close();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
           _binding.SearchItem(TxtSearch.Text);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addItem = new AddUnit();
            var dialogResult = (bool)addItem.ShowDialog();
            if (!dialogResult)
            {
                _binding.FillFields(PersonID);
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var editItem = new AddUnit(_binding.GetSelectedUnit().UnitId);
            var dialogResult = (bool)editItem.ShowDialog();
            if (!dialogResult)
            {
                _binding.FillFields(PersonID);
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this item :{_binding.GetSelectedUnit().CategoryName}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                UnitDelete(_binding.GetSelectedUnit().UnitId);
                MessageBox.Show("Item Deleted");
            }
        }

        private void UnitDelete(int unitId)
        {
            using var context = new CuriosityContext();
            var unitContext = context.Units
                .Find(unitId);

            unitContext.IsDeleted = true;
            context.SaveChanges();

            _binding.Refresh();
        }

        private void BtnConfirmBuy_Click(object sender, RoutedEventArgs e)
        {
            if (CheckQuantity(TxtBuy.Text))
            {
                _binding.BuyItem(PersonID, _binding.SelectedUnit.UnitId, int.Parse(TxtBuy.Text));
                
                GrdBuy.Visibility = Visibility.Collapsed;
                BtnBuy.Visibility = Visibility.Visible;
                TxtBuy.Text = "";

                _binding.FillFields(PersonID);
                DataContext = null;
                DataContext = _binding;
            }
        }

        private bool CheckQuantity(string quantity)
        {
            try
            {
                var x = int.Parse(quantity);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter a numerical quantity");
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_binding.AccessType != "Customer")
            {
                GrdRestricted.Visibility = Visibility.Visible;
            }
        }

        private void LstItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnEdit.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnBuy.Visibility = Visibility.Visible;
            BtnAllItems.Visibility = Visibility.Visible;
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainUserPage(PersonID);
            window.Show();

            Close();
        }
    }

    public class MainItemPageBinding : INotifyPropertyChanged
    {
        private CategoryDto _selectedUnit;
        public ObservableCollection<CategoryDto> UnitList { get; } = new ObservableCollection<CategoryDto>();

        public CategoryDto SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }

        public string PersonName { get; set; }
        public string AccessType { get; set; }

        public void FillFields(int pId)
        {
            using var context = new CuriosityContext();

            var personContext = context.Persons.Find(pId);
            PersonName = $"{personContext.FirstName} {personContext.LastName}";
            AccessType = personContext.AccessType.ToString();

            Refresh();
        }

        public void Refresh()
        {
            using var context = new CuriosityContext();

            var itemContext = context.Units
                .Include(a => a.CategoryLink)
                .Where(a => a.IsDeleted == false)
                .ToList();

            UnitList.Clear();
            foreach (var unit in itemContext)
            {
                var newUnit = new CategoryDto()
                {
                    CategoryName = unit.CategoryLink.CategoryName, 
                    UnitId = unit.UnitId,
                    UnitDescription = unit.UnitDescription,
                    UnitPrice = unit.UnitPrice,
                    UnitQuantity = unit.UnitQuantity
                };

                UnitList.Add(newUnit);
            }
        }

        public void SearchItem(string text)
        {
            using var context = new CuriosityContext();

            var itemContext = context.Units
                .Include(a => a.CategoryLink)
                .Where(a => a.IsDeleted == false && a.CategoryLink.CategoryName.Contains(text))
                .ToList();

            UnitList.Clear();
            foreach (var unit in itemContext)
            {
                var newUnit = new CategoryDto()
                {
                    CategoryName = unit.CategoryLink.CategoryName,
                    UnitId = unit.UnitId,
                    UnitDescription = unit.UnitDescription,
                    UnitPrice = unit.UnitPrice
                };

                UnitList.Add(newUnit);
            }
        }

        public void BuyItem(int pId, int uId, int quantity)
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Find(pId);

            var unitContext = context.Units
                .Include(a => a.CategoryLink)
                .First(a => a.UnitId == uId);

            try
            {
                if (unitContext.UnitQuantity - quantity < 0) throw new Exception();

                unitContext.UnitQuantity -= quantity;

                var buyItem = new SaleUnit()
                {
                    SaleDate = DateAndTime.Now.Date,
                    SaleQuantity = quantity,
                    Total = unitContext.UnitPrice * quantity,
                    IsDeleted = false,

                    UnitId = unitContext.UnitId,
                    UnitLink = unitContext,

                    PersonLink = personContext,
                    PersonId = personContext.PersonId
                };

                context.SaleUnits.Add(buyItem);
                context.SaveChanges();

                MessageBox.Show($"Successfully bought:" +
                                $"\n\tItem Name: {SelectedUnit.CategoryName}" +
                                $"\n\tQuantity: {quantity}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"{unitContext.CategoryLink.CategoryName} has not enough stock");
            }
        }

        public CategoryDto GetSelectedUnit()
        {
            return _selectedUnit;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
