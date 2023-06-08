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
using MessageBox = System.Windows.MessageBox;
using Visibility = System.Windows.Visibility;

namespace UI_QueenAnnes
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class EditNewUser : Window
    {
        private AddNewUserBinding _addNewUser = new AddNewUserBinding();
        private int PersonId { get; }
        private string EditPersonAccess { get; set; }

        //View User
        public EditNewUser(int pId)
        {
            InitializeComponent();
            _addNewUser.EditProfile(pId);
            DataContext = _addNewUser;
            PersonId = pId;
        }

        //View User by using All Person
        public EditNewUser(int pId, string editPerson)
        {
            InitializeComponent();
            _addNewUser.EditProfile(pId, editPerson);

            DataContext = _addNewUser;
            PersonId = pId;
            EditPersonAccess = editPerson;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmpty()) return;

            var signInChk = new Sign_InCheckUp();
            var pAccess = signInChk.CheckCredentials(TxtZIP.Text, TxtPhone.Text, TxtEmail.Text,
                _addNewUser.SelectedAccessType);

            if (pAccess == "Manager" && EmployeeIsEmpty()) return;

            if (_addNewUser.SelectedAccessType == "Manager")
            {
                _addNewUser.SaveEditProfile(PersonId, TxtFName.Text, TxtLName.Text, TxtAddress.Text,
                    TxtZIP.Text, TxtPhone.Text, CmBAccessType.SelectedItem.ToString(), TxtEmail.Text, TxtSalary.Text, DpRecruitDate.Text);
            }
            else
            {
                _addNewUser.SaveEditProfile(PersonId, TxtFName.Text, TxtLName.Text, TxtAddress.Text,
                    TxtZIP.Text, TxtPhone.Text, _addNewUser.SelectedAccessType, TxtEmail.Text, null, null);
            }

            MessageBox.Show("Edit Success");
            DisableProfileFields();
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

        private bool EmployeeIsEmpty()
        {
            if (string.IsNullOrEmpty(TxtSalary.Text))
            {
                MessageBox.Show("Employee salary is empty");
                return true;
            }
            if(string.IsNullOrEmpty(DpRecruitDate.Text))
            {
                MessageBox.Show("Recruitment Date is empty");
                return true;
            }

            try
            {
                int.Parse(TxtSalary.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter a numerical value on their corresponding fields");
                return true;
            }


            return false;
        }

        private void ChkEmployee()
        {
            if (_addNewUser.GetPerson().AccessType != AccessType.Customer.ToString())
            {
                GrdEmployee.Visibility = Visibility.Visible;
                GrdAccess.Visibility = Visibility.Visible;
                BtnView.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(EditPersonAccess)) BtnView.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChkEmployee();
        }

        private void DisableProfileFields()
        {
            LblHeader.Content = "Profile";
            BtnEdit.Visibility = Visibility.Visible;
            BtnSave.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(EditPersonAccess))
            {
                DisableBasicFields();
                TxtEmail.IsEnabled = false;
                BtnChangePass.IsEnabled = false;
                //The account is an employee
                if (_addNewUser.GetPerson().AccessType == "Employee")
                {
                    CmBAccessType.IsEnabled = false;
                }
                //The account is a manager
                else if (_addNewUser.GetPerson().AccessType == "Manager")
                {
                    TxtSalary.IsEnabled = false;
                    CmBAccessType.IsEnabled = false;
                    DpRecruitDate.IsEnabled = false;
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (BtnEdit.Visibility == Visibility.Collapsed)
            {
                DisableProfileFields();
            }
            else
            {
                if (string.IsNullOrEmpty(EditPersonAccess))
                {
                    var window = new MainUserPage(PersonId);
                    window.Show();
                    Close();
                }
                else
                {
                    Close();
                }
            }
        }

        private void BtnChangePass_Click(object sender, RoutedEventArgs e)
        {
            var window = new PersonChangePassword(PersonId);
            window.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Person is editing his/her own profile
            LblHeader.Content = "Edit Profile";
            BtnEdit.Visibility = Visibility.Collapsed;
            BtnSave.Visibility = Visibility.Visible;
            BtnBack.ToolTip = "Cancel Edit";

            if (string.IsNullOrEmpty(EditPersonAccess))
            {
                EnableBasicFields();
                TxtEmail.IsEnabled = true;
                BtnChangePass.IsEnabled = true;

                //if account is manager
                if (_addNewUser.GetPerson().AccessType == "Manager")
                {
                    TxtSalary.IsEnabled = true;
                    CmBAccessType.IsEnabled = true;
                    DpRecruitDate.IsEnabled = true;
                }
            }
            else
            {
                if (_addNewUser.GetPerson().AccessType == "Manager")
                {
                    TxtSalary.IsEnabled = true;
                    CmBAccessType.IsEnabled = true;
                    DpRecruitDate.IsEnabled = true;
                }
            }
        }

        private void DisableBasicFields()
        {
            TxtFName.IsEnabled = false;
            TxtLName.IsEnabled = false;
            TxtAddress.IsEnabled = false;
            TxtZIP.IsEnabled = false;
            TxtPhone.IsEnabled = false;
        }

        private void EnableBasicFields()
        {
            TxtFName.IsEnabled = true;
            TxtLName.IsEnabled = true;
            TxtAddress.IsEnabled = true;
            TxtZIP.IsEnabled = true;
            TxtPhone.IsEnabled = true;
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var viewAllPerson = new AllPerson(PersonId);
            viewAllPerson.Show();

            Close();
        }
    }
}
