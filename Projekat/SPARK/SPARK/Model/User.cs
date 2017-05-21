using System.ComponentModel.DataAnnotations.Schema;

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
                username = value;
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
                password = value;
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
                name = value;
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
                surname = value;
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
                email = value;
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
