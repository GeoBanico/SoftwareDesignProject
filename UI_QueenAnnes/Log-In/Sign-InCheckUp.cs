using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using SoftwareDesign_Project_QueenAnneCuriosityShop;

namespace UI_QueenAnnes.Log_In
{
    public class Sign_InCheckUp
    {

        public string CheckCredentials(string zip, string phone, string email, string access)
        {
            if (CheckEmail(email) && CheckZip(zip) && CheckPhone(phone))
            {
                if (access == "Employee") return AccessType.Employee.ToString();
                else if (access == "Manager") return AccessType.Manager.ToString();
                return AccessType.Customer.ToString();
            }

            return "";
        }

        public bool CheckEmail(string email)
        {
            try
            {
                var emailChecker = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
                if(!emailChecker.IsValid(email)) throw new Exception("Wrong Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    ("Incorrect Email");
                return false;
            }

            return true;
        }

        public bool CheckZip(string zip)
        {
            try
            {
                if (zip.Length < 3 || zip.Length > 5) throw new Exception("Incorrect ZIP code");
                Convert.ToInt32(zip);
            }
            catch (Exception e)
            {
                MessageBox.Show
                    ($"Incorrect ZIP code");
                return false;
            }

            return true;
        }

        public bool CheckPhone(string phone)
        {
            try
            {
                if(phone.Length > 11 || phone.Length < 10) throw new Exception("Incorrect Phone");
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    ($"Phone number maybe to large or to small");
                return false;
            }

            try
            {
                var i = long.Parse(phone);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Please enter a numerical phone number");
                throw;
            }
            return true;
        }
    }
}
