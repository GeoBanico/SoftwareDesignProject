using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI_QueenAnnes.Item;

namespace UI_QueenAnnes.Person
{
    /// <summary>
    /// Interaction logic for MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Window
    {
        private MainUserPageBinding _mainUserPageBinding;
        private int PersonID { get; set; }

        public MainUserPage(int pID)
        {
            InitializeComponent();
            _mainUserPageBinding = new MainUserPageBinding();
            _mainUserPageBinding.SetPerson(pID);
            DataContext = _mainUserPageBinding;

            PersonID = pID;
        }

        private void BtnItems_Click(object sender, RoutedEventArgs e)
        {
            var mainItem = new MainItemPage(PersonID);
            mainItem.Show();
            this.Close();

        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();

            login.Show();
            this.Close();
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditNewUser(PersonID);
            window.Show();
            Close();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LblSearch.Visibility = !string.IsNullOrEmpty(TxtSearch.Text) ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            _mainUserPageBinding.SearchPerson(TxtSearch.Text);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewAllDeleted();
            window.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mainUserPageBinding.PersonAccessType != "Customer") BtnDelete.Visibility = Visibility.Visible;
        }
    }
}
