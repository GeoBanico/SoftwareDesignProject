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
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto;
using UI_QueenAnnes.Annotations;

namespace UI_QueenAnnes.Person
{
    /// <summary>
    /// Interaction logic for PersonRelation.xaml
    /// </summary>
    public partial class PersonRelation : Window
    {
        private PersonRelationBinding _binding = new PersonRelationBinding();
        private int MineId { get; set; }

        public PersonRelation(int cId)
        {
            InitializeComponent();

            _binding.FillFields(cId);
            DataContext = _binding;
            MineId = cId;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LblSearch.Visibility = !string.IsNullOrEmpty(TxtSearch.Text) ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _binding.AddFriend(_binding.GetPerson().PersonId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _binding.DeleteFriend(_binding.GetFriend().PersonId);
            DataContext = null;
            DataContext = _binding;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var allPepBinding = new AllPersonBinding();
            allPepBinding.FillFields();

            Close();
        }
    }

    public class PersonRelationBinding : INotifyPropertyChanged
    {
        private int _myId;
        private string _myName;
        private string _myAccess;

        public int MyId
        {
            get => _myId;
            set
            {
                _myId = value;
                OnPropertyChanged(nameof(MyId));
            }

        }
        public string MyName
        {
            get => _myName;
            set
            {
                _myName = value;
                OnPropertyChanged(nameof(MyName));
            }
        }
        public string MyAccess
        {
            get => _myAccess;
            set
            {
                _myAccess = value;
                OnPropertyChanged(nameof(MyAccess));
            }
        }

        public ObservableCollection<PersonsDto> PersonList { get; } = new ObservableCollection<PersonsDto>();
        private PersonsDto _selectedPerson;
        public PersonsDto SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public ObservableCollection<PersonsDto> FriendsList { get; } = new ObservableCollection<PersonsDto>();
        private PersonsDto _selectedFriend;
        

        public PersonsDto SelectedFriend
        {
            get => _selectedFriend;
            set
            {
                _selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

        public PersonsDto GetPerson()
        {
            return _selectedPerson;
        }

        public PersonsDto GetFriend()
        {
            return _selectedFriend;
        }

        public void FillFields(int myId)
        {
            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Find(myId);

            MyName = $"{personContext.FirstName} {personContext.LastName}";
            MyId = myId;
            MyAccess = personContext.AccessType.ToString();

            FillPersonList(myId);
        }

        public void FillPersonList(int myId)
        {
            using var context = new CuriosityContext();
            var myFriendContext = context.Persons
                .Find(myId);

            var personContext = context.Persons
                .Where(a => a.IsDeleted == false && a.PersonId != myId)
                .ToList();

            PersonList.Clear();
            FriendsList.Clear();
            foreach (var person in personContext)
            {
                var pep = new PersonsDto()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PersonId = person.PersonId
                };

                if (myFriendContext.PersonList.Count > 0)
                {
                    bool friend = false;
                    foreach (var peps in myFriendContext.PersonList)
                    {
                        if (pep.PersonId == peps.PersonId)
                        {
                            FriendsList.Add(pep);
                            friend = true;
                            break;
                        }
                    }

                    if(!friend) PersonList.Add(pep);
                }
                else PersonList.Add(pep);
            }
        }

        public void AddFriend(int pId)
        {
            using var context = new CuriosityContext();

            var personContext = context.Persons
                .Find(pId);

            var myFriendContext = context.Persons
                .Find(MyId);

            myFriendContext.PersonList.Add(personContext);

            context.SaveChanges();

            FillPersonList(MyId);
        }

        public void DeleteFriend(int fId)
        {
            using var context = new CuriosityContext();

            var friendContext = context.Persons
                .Find(MyId);

            var myContext = context.Persons
                .Find(MyId);

            myContext.PersonList.Remove(friendContext);

            context.SaveChanges();

            FillPersonList(MyId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
