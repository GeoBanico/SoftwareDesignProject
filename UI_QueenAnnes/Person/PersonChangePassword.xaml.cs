using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using UI_QueenAnnes.Annotations;
using UI_QueenAnnes.Log_In;

namespace UI_QueenAnnes.Person
{
    /// <summary>
    /// Interaction logic for PersonChangePassword.xaml
    /// </summary>
    public partial class PersonChangePassword : Window
    {
        private PersonChangePasswordBinding _binding = new PersonChangePasswordBinding();
        private int PersonId { get; set; }
        public PersonChangePassword(int pId)
        {
            InitializeComponent();

            _binding.FillFields(pId);
            DataContext = _binding;
            PersonId = pId;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _binding.CheckPassword(TxtPastPassword.Text);
            _binding.ChangePassword(TxtNewPassword.Text);

            MessageBox.Show("Change successful... now returning");
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class PersonChangePasswordBinding : INotifyPropertyChanged
    {
        public int MyId { get; set; }
        public string MyName { get; set; }
        public string MyEmail { get; set; }

        public void FillFields(int pId)
        {
            using var context = new CuriosityContext();
            var myContext = context.Persons
                .Find(pId);

            MyId = myContext.PersonId;
            MyName = $"{myContext.FirstName} {myContext.LastName}";
            MyEmail = myContext.EmailAddress;
        }

        public void CheckPassword(string oldPass)
        {
            using var context = new CuriosityContext();
            var myContext = context.Persons
                .Find(MyId);

            var dBSalt = myContext.Salt;
            var dBPass = myContext.Password;

            var encryptor = new PasswordEncryptor();

            try
            {
                if (encryptor.EncrptPass(dBSalt + oldPass) != dBPass) throw new Exception();
                    
            }
            catch (Exception e)
            {
                MessageBox.Show("Incorrect password");
            }
        }

        public void ChangePassword(string newPass)
        {
            using var context = new CuriosityContext();
            var myContext = context.Persons
                .Find(MyId);

            var encryptor = new PasswordEncryptor();

            var newSalt = encryptor.CreateSalt();

            myContext.Salt = newSalt;
            myContext.Password = encryptor.EncrptPass(newSalt + newPass);

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
