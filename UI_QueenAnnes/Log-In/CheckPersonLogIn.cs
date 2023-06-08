using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using UI_QueenAnnes.Person;

namespace UI_QueenAnnes.Log_In
{
    public class CheckPersonLogIn
    {
        public string CheckPerson(string user, string pass)
        {
            using var context = new CuriosityContext();
            var decrypt = new PasswordEncryptor();
            var peps = context.Persons
                .Where(a => a.EmailAddress.ToLower() == user.ToLower() && a.IsDeleted == false)
                .ToList();

            if (peps.Count == 0) return "";
            foreach (var pep in peps)
            {
                var salt = pep.Salt;
                if (pep.Password == decrypt.EncrptPass(salt + pass))
                {
                    return pep.PersonId.ToString();
                }
            }

            return "";
        }
    }
}
