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
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using UI_QueenAnnes.Log_In;
using UI_QueenAnnes.Person;

namespace UI_QueenAnnes
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        private AddNewUserBinding userBinding = new AddNewUserBinding();


        private string AccessTypes { get; set; } = null;
        public AddNewUser()
        {
            InitializeComponent();
            userBinding.AccessTypeBind("");
            DataContext = userBinding;
        }
        
        public AddNewUser(string accessType)
        {
            InitializeComponent();
            userBinding.AccessTypeBind(accessType);
            DataContext = userBinding;
            PageRestrictions(accessType);
            AccessTypes = accessType;
        }

        private void PageRestrictions(string accessType)
        {
            if (accessType == "Manager")
            {
                GrdAccess.Visibility = Visibility.Visible;
                GrdEmployee.Visibility = Visibility.Visible;
            }
            else if (accessType == "Employee") GrdAccess.Visibility = Visibility.Visible;
        }

        private AccessType Selecter(string sign)
        {
            if (sign == AccessType.Manager.ToString()) return AccessType.Manager;
            else if (sign == AccessType.Employee.ToString()) return AccessType.Employee;
            return AccessType.Customer;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmpty()) return;

            var signInChk = new Sign_InCheckUp();
            var inSignIn = signInChk.CheckCredentials(TxtZIP.Text, TxtPhone.Text, TxtEmail.Text,
                CmBAccessType.SelectedItem.ToString());

            if (inSignIn == "") return;
            var signIn = Selecter(inSignIn);

            using var context = new CuriosityContext();
            var encryptClass = new PasswordEncryptor();

            string salt = encryptClass.CreateSalt();
            string encryptedPass = encryptClass.EncrptPass(salt + TxtPassword.Text);

            var newPerson = new SoftwareDesign_Project_QueenAnneCuriosityShop.Person()
            {
                LastName = TxtLName.Text,
                FirstName = TxtFName.Text,
                Address = TxtAddress.Text,
                ZIP = Convert.ToInt32(TxtZIP.Text),
                Phone = TxtPhone.Text,
                EmailAddress = TxtEmail.Text,
                Password = encryptedPass,
                Salt = salt,
                IsDeleted = false,
                AccessType = signIn
            };

            if (CmBAccessType.SelectionBoxItem.ToString() != "Customer" && !string.IsNullOrEmpty(TxtSalary.Text) &&
                !string.IsNullOrEmpty(GrdEmployee.ToString()))
            {
                if (!CheckEmployee(TxtSalary.Text)) return;
                var newEmployee = new Employee()
                {
                    Salary = float.Parse(TxtSalary.Text),
                    RecruitmentDate = DateTime.Parse(DpRecruitDate.Text)
                };

                newPerson.EmployeeLink = newEmployee;
            }

            context.Persons.Add(newPerson);
            context.SaveChanges();

            MessageBox.Show(string.IsNullOrEmpty(AccessTypes)
                ? "Sign-Up successful, please log-in"
                : "Add successful... now returning");
            Close();

        }

        public bool CheckEmployee(string salary)
        {
            try
            { 
                
                float.Parse(salary);
            }
            catch (Exception e)
            {
                MessageBox.Show("Salary Error, please enter a number");
                return false;
            }

            return true;
        }

        private bool IsEmpty()
        {
            if (string.IsNullOrEmpty(TxtFName.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtLName.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtAddress.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtZIP.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }
            if (string.IsNullOrEmpty(TxtPhone.Text))
            {
                MessageBox.Show($"Please fill up the empty fields");
                return true;
            }

            return false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
