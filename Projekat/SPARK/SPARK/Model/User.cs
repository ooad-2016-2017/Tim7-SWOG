using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SPARK.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
