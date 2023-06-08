using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Compression;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop.Annotations;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop
{
    public class Person
    {
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public int ZIP { get; set; }
        public string Phone { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsDeleted { get; set; }
        public AccessType AccessType { get; set; }

        public ICollection<Person> PersonList { get; set; } = new List<Person>();
        public ICollection<SaleUnit> SaleItemList { get; set; }

        //public int? PersonId { get; set; }
        public Person PersonLink { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee EmployeeLink { get; set; }
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public float Salary { get; set; }
        public DateTime RecruitmentDate { get; set; }
        public bool IsDeleted { get; set; }

        //[ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person PersonLink { get; set; }

        public ICollection<ReceivedStatus> ReceivedStatusList { get; set; }
    }

    public enum AccessType
    {
        Customer,
        Employee,
        Manager
    }

    /*
    public class PersonRole
    {
        public int PersonRoleId { get; set; }
        public int  RoleId { get; set; }
        public DateTime DateGranted { get; set; }

        public int PersonId { get; set; }
        public Person PersonLink { get; set; }

        public ICollection<Role> RoleList { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleDesc { get; set; }

        public int PersonRoleId { get; set; }
        public PersonRole PersonRoleLink { get; set; }

        public ICollection<Access> AccessLink { get; set; }

    }

    public class Access
    {
        public int AccessId { get; set; }
        public DateTime DateGranted { get; set; }

        public int RoleId { get; set; }
        public Role RoleLink { get; set; }
        public int PermissionId { get; set; }
        public AccessType AccessTypeLink { get; set; }
    }

    public class AccessType
    {
        public int AccessTypeId { get; set; }
        public string TypeDesc { get; set; }

        public ICollection<Access> AccessList { get; set; }
    }
    */
}
