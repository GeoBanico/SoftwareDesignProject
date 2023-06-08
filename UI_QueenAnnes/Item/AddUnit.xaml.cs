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
using SoftwareDesign_Project_QueenAnneCuriosityShop.UnitDto;
using UI_QueenAnnes.Annotations;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace UI_QueenAnnes.Item
{
    /// <summary>
    /// Interaction logic for AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        private AddUnitBinding _UnitBinding = new AddUnitBinding();
        private string UnitId { get; set; } = null;
        public AddUnit()
        {
            InitializeComponent();

            _UnitBinding.FillCategory("");
            DataContext = _UnitBinding;
        }

        public AddUnit(int uId)
        {
            InitializeComponent();

            _UnitBinding.EditFillFields(uId);
            DataContext = _UnitBinding;

            UnitId = uId.ToString();
        }

        private void BtnAddCatName_Click(object sender, RoutedEventArgs e)
        {
            GrdCategory.Visibility = Visibility.Visible;
            TxtCategoryName.Text = _UnitBinding.SelectedCat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmpty()) return;
            if (string.IsNullOrEmpty(UnitId)) _UnitBinding.AddToSave(float.Parse(TxtPrice.Text), TxtDesc.Text, 0, CmbCatName.SelectedItem.ToString());
            else _UnitBinding.EditToSave(float.Parse(TxtPrice.Text), TxtDesc.Text, _UnitBinding.GetCategory().UnitQuantity, CmbCatName.SelectedItem.ToString(), int.Parse(UnitId));

            Close();
        }

        private bool CheckEmpty()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtDesc.Text)) throw new Exception();
                if(string.IsNullOrEmpty(TxtPrice.Text)) throw new Exception();
                if(CmbCatName.SelectedIndex == -1) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            try
            {
                var x =float.Parse(TxtPrice.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("The inputted price has non-numerical values" +
                                "\n\tplease input numerical values in the price fields");
                return false;
            }

            return true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Category Functionality
        private bool IsNewCategory { get; set; }

        private void BtnCatAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtCategoryName.Text = "";
            TxtCategoryName.IsEnabled = true;
            BtnViewCatName.IsEnabled = false;

            HideAddEditDelete();
            IsNewCategory = true;
        }

        private string OldCategoryName { get; set; } = new string("");
        private void BtnCatEdit_Click(object sender, RoutedEventArgs e)
        {
            TxtCategoryName.IsEnabled = true;
            BtnViewCatName.IsEnabled = false;

            HideAddEditDelete();
            OldCategoryName = TxtCategoryName.Text;
            IsNewCategory = false;
        }

        private void BtnCatDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete {CmbCatName.SelectedItem.ToString()} Category?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _UnitBinding.CategoryDelete(_UnitBinding.SelectedCat);
                MessageBox.Show("Category Deleted");
            }

            ReturnToNormal();
        }

        private void BtnCatSave_Click(object sender, RoutedEventArgs e)
        {
            if(!CatChkFields()) return;

            if (IsNewCategory) //Add
            {
                _UnitBinding.AddNewCategory(TxtCategoryName.Text);
                ReturnToNormal();
            }
            else //Edit
            {
                _UnitBinding.EditNewCategory(OldCategoryName, TxtCategoryName.Text);
                ReturnToNormal();
            }
        }

        private void BtnCatBack_Click(object sender, RoutedEventArgs e)
        {
            ShowAddEditDelete();
        }

        private bool CatChkFields()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtCategoryName.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up empty fields.");
                return false;
            }

            return true;
        }

        private void ReturnToNormal()
        {
            _UnitBinding.FillCategory("");

            var holdChange = new[] { TxtPrice.Text, TxtDesc.Text };

            DataContext = null;
            DataContext = _UnitBinding;

            ReturnChanges(holdChange);
            TxtCategoryName.IsEnabled = false;

            ShowAddEditDelete();
        }

        private void ReturnChanges(string[] holdChange)
        {
            TxtPrice.Text = holdChange[0];
            TxtDesc.Text = holdChange[1];
        }

        private void HideAddEditDelete()
        {
            BtnCatAdd.Visibility = Visibility.Collapsed;
            BtnCatEdit.Visibility = Visibility.Collapsed;
            BtnCatDelete.Visibility = Visibility.Collapsed;

            BtnCatSave.Visibility = Visibility.Visible;
        }

        private void ShowAddEditDelete()
        {
            BtnCatAdd.Visibility = Visibility.Visible;
            BtnCatEdit.Visibility = Visibility.Visible;
            BtnCatDelete.Visibility = Visibility.Visible;

            BtnCatSave.Visibility = Visibility.Collapsed;

            GrdCategory.Visibility = Visibility.Collapsed;
            BtnViewCatName.IsEnabled = true;
        }
    }

    public class AddUnitBinding : INotifyPropertyChanged
    {
        //UNIT
        private CategoryDto _selectedUnit;
        private string _selectedCat;

        public List<string> CategoryLists { get; set; } = new List<string>();

        public CategoryDto SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }

        public void FillCategory(string uId)
        {
            using var context = new CuriosityContext();
            var catContext = context.Categories
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.CategoryName)
                .ToList();

            CategoryLists.Clear();
            foreach (var cat in catContext)
            {
                CategoryLists.Add(cat.CategoryName);
            }


            if (string.IsNullOrEmpty(uId)) if (CategoryLists.Count > 0) SelectedCat = CategoryLists[0];
        }

        public void EditFillFields(int uId)
        {
            using var context = new CuriosityContext();
            var unitContext = context.Units
                .Include(a => a.CategoryLink)
                .First(a => a.UnitId == uId);

            SelectedUnit = new CategoryDto()
            {
                CategoryName = unitContext.CategoryLink.CategoryName,
                UnitPrice = unitContext.UnitPrice,
                UnitDescription = unitContext.UnitDescription,
                UnitQuantity = unitContext.UnitQuantity
            };

            SelectedCat = SelectedUnit.CategoryName;
            FillCategory(uId.ToString());
        }

        public void AddToSave(float price, string desc, int quan, string cat)
        {
            using var context = new CuriosityContext();
            var findCat = context.Categories
                .First(a => a.CategoryName == cat);

            var newUnit = new Unit()
            {
                UnitPrice = price,
                UnitDescription = desc,
                UnitQuantity = quan,

                CategoryId = findCat.CategoryId,
                CategoryLink = findCat,

                IsDeleted = false
            };

            context.Units.Add(newUnit);
            context.SaveChanges();

            MessageBox.Show("Add successful... now returning");
        }

        public void EditToSave(float price, string desc, int quan, string cat, int uId)
        {
            using var context = new CuriosityContext();
            var findCat = context.Categories
                .First(a => a.CategoryName == cat);

            var unitToEdit = context.Units
                .Find(uId);

            unitToEdit.UnitPrice = price;
            unitToEdit.UnitQuantity = quan;
            unitToEdit.UnitDescription = desc;

            unitToEdit.CategoryLink = findCat;
            unitToEdit.CategoryId = findCat.CategoryId;

            context.SaveChanges();
            MessageBox.Show("Add successful... now returning");
        }

        public CategoryDto GetCategory()
        {
            return _selectedUnit;
        }

        //CATEGORY
        public string SelectedCat
        {
            get => _selectedCat;
            set
            {
                _selectedCat = value;
                OnPropertyChanged(nameof(SelectedCat));
            }
        }

        public void EditNewCategory(string oldCat, string newCat)
        {
            using var context = new CuriosityContext();

            try
            {
                var categoryFindContext = context.Categories
                    .Where(a => a.CategoryName.ToLower() == newCat.ToLower())
                    .ToList();

                if(categoryFindContext.Count > 0) throw new Exception();

                var categoryContext = context.Categories.First(a => a.CategoryName == oldCat);

                categoryContext.CategoryName = newCat;

                context.SaveChanges();

                MessageBox.Show("Category edit successful");
            }
            catch (Exception e)
            {
                MessageBox.Show("Category is already available, please enter a new category");
            }
        }

        public void AddNewCategory(string newCat)
        {
            using var context = new CuriosityContext();

            var categoryContext = context.Categories
                .Where(a => a.CategoryName.ToLower() == newCat.ToLower())
                .ToList();

            try
            {
                if (categoryContext.Count == 0)
                {
                    var newCategory = new Category()
                    {
                        CategoryName = newCat,
                        IsDeleted = false
                    };
                    context.Categories.Add(newCategory);
                }
                else throw new Exception();

                context.SaveChanges();
                MessageBox.Show("new category added");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Category is already available, please enter a new category");
            }
        }

        public void CategoryDelete(string catName)
        {
            using var context = new CuriosityContext();
            var deleteCat = context.Categories
                .First(a => a.CategoryName == catName);

            deleteCat.IsDeleted = true;

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
