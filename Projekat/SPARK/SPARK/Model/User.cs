using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SPARK.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int id;
        private string username;
        private string password;
        private string name;
        private string surname;
        private string email;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                if (value != "") username = value;
                else throw new Exception("Username nije unesen");

            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                if (value != "") surname = value;
                else throw new Exception("Prezime nije uneseno");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                /*string kriptovana = "";
                StringBuilder hash = new StringBuilder();
                MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(value));

                for (int i = 0; i < bytes.Length; i++)
                {
                    hash.Append(bytes[i].ToString("x2"));
                }
                kriptovana = hash.ToString();*/

                if (value != "") password = value;
                else throw new Exception("Password nije unesen");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != "") name = value;
                else throw new Exception("Ime nije uneseno");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                if (new EmailAddressAttribute().IsValid(value)) email = value;
                else throw new Exception("Email nije validan");
            }
        }

        public bool buyCredits()
        {
            return true;
        }
        public bool editProfile()
        {
            return true;
        }
    }
}
