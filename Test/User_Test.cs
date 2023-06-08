using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using UI_QueenAnnes.Log_In;

namespace Test
{
    public class User_Test
    {

        [Test]
        public void AddUserToDatabase()
        {
            using var context = new CuriosityContext();

            var user = new Person()
            {
                FirstName = "Sample",
                LastName = "Sample",
                Address = "Sample Place Somewhere",
                ZIP = 8000,
                Phone = "09121212122",

                EmailAddress = "user@gmail.com",
                Password = "admin123",
                Salt = "abc",
                AccessType = AccessType.Employee,

                IsDeleted = false,
            };

            // act
            context.Persons.Add(user);
            context.SaveChanges();

            Action act = () => context.SaveChanges();

            // assert
            act.Should().NotThrow<DbUpdateException>();
        }

        [TestCase("8000","09228368913","geo@gmail.com","")]
        public void CheckCredentialsTest_Correct(string zip, string phone, string email, string access)
        {
            //arrange
            var x = new Sign_InCheckUp();

            //act
            Action act = () => x.CheckCredentials(zip, phone, email, access);
            var a = x.CheckCredentials(zip, phone, email, access);
            //assert
            act?.Should().NotThrow();
            a.Should().Be(AccessType.Customer.ToString());
        }

        [TestCase("80", "09228368913", "geo@gmail.com", "")]
        public void CheckCredentialsTest_IncorrecZIP(string zip, string phone, string email, string access)
        {
            //arrange
            var x = new Sign_InCheckUp();

            //act
            Action act = () => x.CheckCredentials(zip, phone, email, access);
            var a = x.CheckCredentials(zip, phone, email, access);
            //assert
            act?.Should().Throw<Exception>();
            a.Should().Be(AccessType.Customer.ToString());
        }

        [TestCase("8000", "09228368913", "2", "")]
        public void CheckCredentialsTest_IncorrectEmail(string zip, string phone, string email, string access)
        {
            //arrange
            var x = new Sign_InCheckUp();

            //act
            Action act = () => x.CheckCredentials(zip, phone, email, access);
            var a = x.CheckCredentials(zip, phone, email, access);
            //assert
            act?.Should().Throw<Exception>();
            a.Should().Be(AccessType.Customer.ToString());
        }

        [TestCase("8000", "092283", "geo@gmail.com", "")]
        public void CheckCredentialsTest_IncorrectPhone(string zip, string phone, string email, string access)
        {
            //arrange
            var x = new Sign_InCheckUp();

            //act
            Action act = () => x.CheckCredentials(zip, phone, email, access);
            var a = x.CheckCredentials(zip, phone, email, access);
            //assert
            act?.Should().Throw<Exception>();
            a.Should().Be(AccessType.Customer.ToString());
        }

        [Test]
        public void PersonEmployeeConnection()
        {
            using var context = new CuriosityContext();

            var passEncrypt = new PasswordEncryptor();
            var salt = passEncrypt.CreateSalt();
            var pass = passEncrypt.EncrptPass(salt + "employee123");

            var user = new Person()
            {
                FirstName = "SaGe",
                LastName = "Banico",
                Address = "Davao CIty",
                ZIP = 8000,
                Phone = "09982919212",

                EmailAddress = "sage@gmail.com",
                Password = pass,
                Salt = salt,
                AccessType = AccessType.Employee,

                IsDeleted = false,
            };

            var employee = new Employee()
            {
                Salary = 8000,
                RecruitmentDate = DateTime.Parse("2011-12-23"),
                IsDeleted = false
            };

            user.EmployeeLink = employee;

            // act
            context.Persons.Add(user);
            context.SaveChanges();

            Action act = () => context.SaveChanges();

            // assert
            act.Should().NotThrow<DbUpdateException>();
        }

        [Test]

        public void GetEmployeeFromPerson()
        {
            using var context = new CuriosityContext();

            var person = context.Persons.ToList();

            foreach (var pep in person)
            {
                context.Entry(pep).Reference(c => c.EmployeeLink).Load();

                string employee = pep.EmployeeLink == null ? "-" : pep.EmployeeLink.Salary.ToString();

                Console.WriteLine($"{pep.FirstName} {pep.LastName} | P{employee}");
            }
        }

        [Test]
        public void AddManagerToDatabase()
        {
            using var context = new CuriosityContext();

            var passEncrypt = new PasswordEncryptor();
            var salt = passEncrypt.CreateSalt();
            var pass = passEncrypt.EncrptPass(salt + "admin123");

            var user = new Person()
            {
                FirstName = "Geo",
                LastName = "Banico",
                Address = "Davao CIty",
                ZIP = 8000,
                Phone = "09221112212",

                EmailAddress = "GeoBanico@gmail.com",
                Password = pass,
                Salt = salt,
                AccessType = AccessType.Manager,

                IsDeleted = false,
            };

            var employee = new Employee()
            {
                Salary = 20000,
                RecruitmentDate = DateTime.Parse("2010-12-23"),
                IsDeleted = false
            };

            user.EmployeeLink = employee;

            // act
            context.Persons.Add(user);
            context.SaveChanges();

            Action act = () => context.SaveChanges();

            // assert
            act.Should().NotThrow<DbUpdateException>();
        }
    }
}
