using Panel.Dal.Concrete.EntityFramework.Repository;
using Panel.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Panel.Bll
{
    public class TicketGenerator
    {


        private static Random random = new Random();


        public string CreateUserCode()
        {



            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public string CreateSupportCode()
        {



            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = "#" + Convert.ToString(new String(stringChars));


            return finalString;
        }






    }
}
