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
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto;
using UI_QueenAnnes.Annotations;

namespace UI_QueenAnnes.Person
{
    /// <summary>
    /// Interaction logic for AllPerson.xaml
    /// </summary>
    public partial class AllPerson : Window
    {
        private int PersonId { get; }
        private AllPersonBinding _binding = new AllPersonBinding();
        public AllPerson(int pID)
        {
            InitializeComponent();
            _binding.PersonFillFields(pID);
            DataContext = _binding;
            PersonId = pID;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LblSearch.Visibility = !string.IsNullOrEmpty(TxtSearch.Text) ? Visibility.Hidden : Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _binding.SearchPerson(TxtSearch.Text);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var AddPerson = new AddNewUser(_binding.AccessType);
            var dialogResult = (bool)AddPerson.ShowDialog();
            if (!dialogResult)
            {
                _binding.FillFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var editPerson = new EditNewUser(_binding.getPersonId().PersonId, _binding.AccessType);
            var dialogResult = (bool) editPerson.ShowDialog();
            if(!dialogResult)
            {
                _binding.FillFields();
                DataContext = null;
                DataContext = _binding;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete {LblFName.Content} {LblLName.Content}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                PersonDelete(_binding.getPersonId().PersonId);
                MessageBox.Show("Person Deleted");
            }

            Refresh();
            _binding.PersonFillFields(PersonId);
            DataContext = null;
            DataContext = _binding;
        }

        private void PersonDelete(int cId)
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Find(cId);

            personContext.IsDeleted = true;
            context.SaveChanges();
        }

        private void Refresh()
        {
            TxtSearch.Text = "";
            _binding.FillFields();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditNewUser(PersonId);
            window.Show();
            Close();
        }

        private void LstName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnEdit.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnRelView.Visibility = Visibility.Visible;

            if(_binding.getPersonId() != null) GrdEmployee.Visibility = !string.IsNullOrEmpty(_binding.getPersonId().RecruitmentDate.ToString()) ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnRelView_Click(object sender, RoutedEventArgs e)
        {
            var window = new PersonRelation(_binding.getPersonId().PersonId);
            var dialogResult = (bool)window.ShowDialog();
            if (!dialogResult)
            {
                _binding.FillFields();
                DataContext = null;
                DataContext = _binding;
            }
        }
    }

    public class AllPersonBinding : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string AccessType { get; set; }
        public ObservableCollection<PersonsDto> PersonList { get; } = new ObservableCollection<PersonsDto>();

        private PersonsDto _selectedPerson;
        private PersonsDto _selectedFriend;

        public PersonsDto SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public PersonsDto SelectedFriend
        {
            get => _selectedFriend;
            set
            {
                _selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

        public void PersonFillFields(int pId)
        {
            using var context = new CuriosityContext();

            //Find the login person's credentials
            var myContext = context.Persons
                .Find(pId);

            Name = $"{myContext.FirstName} {myContext.LastName}";
            AccessType = myContext.AccessType.ToString();

            FillFields();
        }

        public void FillFields()
        {
            using var context = new CuriosityContext();

            //Find all persons

            var personContext = context.Persons
                .Include(a => a.EmployeeLink)
                .Where(a => (a.IsDeleted == false))
                .ToList();

            PersonList.Clear();
            foreach (var person in personContext)
            {
                var pep = new PersonsDto();
                if (person.AccessType.ToString() == "Customer")
                {
                    pep = new PersonsDto()
                    {
                        PersonId = person.PersonId,
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                        Address = person.Address,
                        ZIP = person.ZIP,
                        Phone = person.Phone,
                        AccessType = person.AccessType.ToString(),
                    };
                    if(person.PersonList.Count > 0) //if there are relationships
                    {
                        foreach (var fpeps in person.PersonList)
                        {
                            var friendPep = new PersonsDto()
                            {
                                PersonId = fpeps.PersonId,
                                LastName = fpeps.LastName,
                                FirstName = fpeps.FirstName,
                                Address = fpeps.Address,
                                ZIP = fpeps.ZIP,
                                Phone = fpeps.Phone
                            };

                            pep.PersonList.Add(friendPep);
                        }
                    }
                }
                else
                {
                    pep = new PersonsDto()
                    {
                        PersonId = person.PersonId,
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                        Address = person.Address,
                        ZIP = person.ZIP,
                        Phone = person.Phone,
                        AccessType = person.AccessType.ToString(),

                        EmailAddress = person.EmailAddress,
                        Salary = person.EmployeeLink.Salary,
                        RecruitmentDate = person.EmployeeLink.RecruitmentDate
                    };
                    if (person.PersonList.Count > 0)
                    {
                        foreach (var fpeps in person.PersonList)
                        {
                            var friendPep = new PersonsDto()
                            {
                                PersonId = fpeps.PersonId,
                                LastName = fpeps.LastName,
                                FirstName = fpeps.FirstName,
                                Address = fpeps.Address,
                                ZIP = fpeps.ZIP,
                                Phone = fpeps.Phone
                            };

                            pep.PersonList.Add(friendPep);
                        }
                    }
                }
                PersonList.Add(pep);
            }
        }

        public void SearchPerson(string search)
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Include(a => a.EmployeeLink)
                .Where(a => (a.IsDeleted == false) && (a.FirstName.Contains(search) || a.LastName.Contains(search)))
                .OrderBy(a => a.FirstName)
                .ToList();

            PersonList.Clear();
            foreach (var person in personContext)
            {
                var pep = new PersonsDto();
                if (person.AccessType.ToString() == "Customer")
                {
                    pep = new PersonsDto()
                    {
                        PersonId = person.PersonId,
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                        Address = person.Address,
                        ZIP = person.ZIP,
                        Phone = person.Phone,
                        AccessType = person.AccessType.ToString(),
                    };
                    if (person.PersonList != null)
                    {
                        pep.PersonList.Clear();
                        foreach (var fpeps in pep.PersonList)
                        {
                            var friendPep = new PersonsDto()
                            {
                                PersonId = fpeps.PersonId,
                                LastName = fpeps.LastName,
                                FirstName = fpeps.FirstName,
                                Address = fpeps.Address,
                                ZIP = fpeps.ZIP,
                                Phone = fpeps.Phone,
                                AccessType = fpeps.AccessType
                            };

                            pep.PersonList.Add(friendPep);
                        }
                    }
                }
                else
                {
                    pep = new PersonsDto()
                    {
                        PersonId = person.PersonId,
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                        Address = person.Address,
                        ZIP = person.ZIP,
                        Phone = person.Phone,
                        AccessType = person.AccessType.ToString(),

                        EmailAddress = person.EmailAddress,
                        Salary = person.EmployeeLink.Salary,
                        RecruitmentDate = person.EmployeeLink.RecruitmentDate
                    };
                    if (person.PersonList.Count > 0)
                    {
                        foreach (var fpeps in person.PersonList)
                        {
                            var friendPep = new PersonsDto()
                            {
                                PersonId = fpeps.PersonId,
                                LastName = fpeps.LastName,
                                FirstName = fpeps.FirstName,
                                Address = fpeps.Address,
                                ZIP = fpeps.ZIP,
                                Phone = fpeps.Phone
                            };

                            pep.PersonList.Add(friendPep);
                        }
                    }
                }
                PersonList.Add(pep);
            }
        }

        public PersonsDto getPersonId()
        {
            return _selectedPerson;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
