using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARK.Model
{
    public class User
    {
        protected int id;
        protected string username;
        protected string password;
        protected string name;
        protected string surname;
        protected string email;

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
