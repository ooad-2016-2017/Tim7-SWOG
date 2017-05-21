using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SPARK.Model
{
    public class PaymentMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int id;
        private string name;
        private string description;
        private List<Tuple<int, double>> packages;

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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public List<Tuple<int, double>> Packages
        {
            get
            {
                return packages;
            }

            set
            {
                packages = value;
            }
        }
    }
}
