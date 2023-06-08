using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop.Annotations;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto
{
    public class PersonsDto : INotifyPropertyChanged
    {
        //From Person
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public int ZIP { get; set; }
        public string Phone { get; set; }
        public string AccessType { get; set; }

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        //From Employee
        public float? Salary { get; set; }
        public DateTime? RecruitmentDate { get; set; }

        public ObservableCollection<PersonsDto> PersonList { get; set; } = new ObservableCollection<PersonsDto>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
