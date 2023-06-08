using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using SoftwareDesign_Project_QueenAnneCuriosityShop;

namespace UI_QueenAnnes.Log_In 
{
    public class PasswordEncryptor
    {
        public string EncrptPass(string pass)
        {
            var hasher = new SHA256Managed();

            var newPass = hasher.ComputeHash(Encoding.Default.GetBytes(pass));

            return Convert.ToBase64String(newPass);
        }

        public string CreateSalt()
        {
            var sb = new StringBuilder();

            var random = new Random();

            int Chars = 3;

            for (int i = 0; i < Chars; i++)
                sb.Append((char)random.Next(33, 123));

            return sb.ToString();
        }
    }
}
