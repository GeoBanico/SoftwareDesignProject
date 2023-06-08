using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SoftwareDesign_Project_QueenAnneCuriosityShop;

namespace Test
{
    
    class Login_Test
    {
        [TestCase("user@gmail.com","admin123")]
        public void searchUser(string user, string pass1)
        {
            using var context = new CuriosityContext();
            var peps = context.Persons.ToList();
            string pass = "";
            foreach (var pep in peps)
            {
                if(pep.EmailAddress == user) pass = pep.Password;
            }

            pass.Should().Be(pass1);
        }

        [TestCase("user@gmail.com", "admin12")]
        public void searchUser_Incorrect(string user, string pass1)
        {
            using var context = new CuriosityContext();
            var peps = context.Persons.ToList();
            string pass = "";
            foreach (var pep in peps)
            {
                if (pep.EmailAddress == user) pass = pep.Password;
            }

            pass.Should().NotBe(pass1);
        }
    }
}
