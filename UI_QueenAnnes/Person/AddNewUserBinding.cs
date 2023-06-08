using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto;
using UI_QueenAnnes.Annotations;

namespace UI_QueenAnnes.Person
{
    class AddNewUserBinding : INotifyPropertyChanged
    {
        private PersonsDto _selectedPerson = new PersonsDto();

        public string[] accessType { get; set; }
        public string SelectedAccessType { get; set; }
        public string EditAccessType { get; set; }

        public PersonsDto SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public void AccessTypeBind(string access)
        {
            accessType = access switch
            {
                "Manager" => new[] {"Customer", "Employee", "Manager"},
                "Employee" => new[] {"Customer", "Employee" },
                _ => new[] {"Customer"}
            };
        }

        //From MyPerson Page
        public void EditProfile(int pId)
        {
            using var context = new CuriosityContext();
            var personProfile = context.Persons
                .Include(a => a.EmployeeLink)
                .First(a => a.PersonId == pId);

            if (personProfile.AccessType.ToString() == "Customer")
            {
                SelectedPerson = new PersonsDto()
                {
                    FirstName = personProfile.FirstName,
                    LastName = personProfile.LastName,
                    Address = personProfile.Address,
                    ZIP = personProfile.ZIP,
                    Phone = personProfile.Phone,

                    EmailAddress = personProfile.EmailAddress,
                    AccessType = personProfile.AccessType.ToString(),
                };
            }
            else
            {
                SelectedPerson = new PersonsDto()
                {
                    FirstName = personProfile.FirstName,
                    LastName = personProfile.LastName,
                    Address = personProfile.Address,
                    ZIP = personProfile.ZIP,
                    Phone = personProfile.Phone,

                    EmailAddress = personProfile.EmailAddress,
                    AccessType = personProfile.AccessType.ToString(),

                    Salary = personProfile.EmployeeLink.Salary,
                    RecruitmentDate = personProfile.EmployeeLink.RecruitmentDate
                };
            }

            AccessTypeBind(SelectedPerson.AccessType);
            SelectedAccessType = SelectedPerson.AccessType;
        }

        //From AllPersonPage
        public void EditProfile(int pId, string editPersonAccess)
        {
            using var context = new CuriosityContext();
            var personProfile = context.Persons
                .Include(a => a.EmployeeLink)
                .First(a => a.PersonId == pId);

            if (personProfile.AccessType.ToString() == "Customer")
            {
                SelectedPerson = new PersonsDto()
                {
                    FirstName = personProfile.FirstName,
                    LastName = personProfile.LastName,
                    Address = personProfile.Address,
                    ZIP = personProfile.ZIP,
                    Phone = personProfile.Phone,

                    EmailAddress = personProfile.EmailAddress,
                    AccessType = personProfile.AccessType.ToString(),
                };
            }
            else
            {
                SelectedPerson = new PersonsDto()
                {
                    FirstName = personProfile.FirstName,
                    LastName = personProfile.LastName,
                    Address = personProfile.Address,
                    ZIP = personProfile.ZIP,
                    Phone = personProfile.Phone,

                    EmailAddress = personProfile.EmailAddress,
                    AccessType = personProfile.AccessType.ToString(),

                    Salary = personProfile.EmployeeLink.Salary,
                    RecruitmentDate = personProfile.EmployeeLink.RecruitmentDate
                };
            }

            AccessTypeBind(editPersonAccess);
            SelectedAccessType = SelectedPerson.AccessType;
            EditAccessType = editPersonAccess;
        }

        public void SaveEditProfile(int pId, string fname, string lname, string address, string zip,
        string phone, string access, string email, [CanBeNull] string salary, [CanBeNull] string recruitDate)
        {

            //For Persons
            using var context = new CuriosityContext();
            var personContext = context.Persons
                    .Find(pId);

            context.Dispose();

            using var newContext = new CuriosityContext();
            personContext.FirstName = fname;
            personContext.LastName = lname;
            personContext.Address = address;
            personContext.ZIP = int.Parse(zip);
            personContext.Phone = phone;
            personContext.EmailAddress = email;

            var eId = personContext.EmployeeId;

            personContext.AccessType = access switch
            {
                "Customer" => AccessType.Customer,
                "Employee" => AccessType.Employee,
                "Manager" => AccessType.Manager,
                _ => personContext.AccessType
            };

            newContext.Entry(personContext).State = EntityState.Modified;
            newContext.SaveChanges();

            //For Employee
            if (access == "Manager")
            {
                using var emContext = new CuriosityContext();
                var editEmployee = emContext.Employees.Find(eId);
                emContext.Dispose();

                using var newEmContext = new CuriosityContext();
                editEmployee.Salary =  float.Parse(salary);
                editEmployee.RecruitmentDate = DateTime.Parse(recruitDate);

                newEmContext.Entry(editEmployee).State = EntityState.Modified;
                newEmContext.SaveChanges();
            }
        }

        public PersonsDto GetPerson()
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
