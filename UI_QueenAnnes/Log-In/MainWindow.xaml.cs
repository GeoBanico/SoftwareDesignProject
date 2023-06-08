using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_QueenAnnes.Log_In;
using UI_QueenAnnes.Person;


namespace UI_QueenAnnes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtPassword.Password)||string.IsNullOrEmpty(TxtUsername.Text))
            {
                MessageBox.Show("Empty username or password", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                var chkPersonClass = new CheckPersonLogIn();
                var personId = chkPersonClass.CheckPerson(TxtUsername.Text, TxtPassword.Password);

                if (personId != "")
                {
                    var mainPage = new MainUserPage(Convert.ToInt32(personId));
                    mainPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect email or pass");
                    Clear();
                }
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var addNewPerson = new AddNewUser();
            addNewPerson.Show();
        }

        private void Clear()
        {
            TxtPassword.Password = "";
            TxtUsername.Text = "";
            TxtUsername.Focus();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            BtnLogIn.Visibility = Visibility.Collapsed;
            GrdLogIn.Visibility = Visibility.Visible;
        }
    }
}
