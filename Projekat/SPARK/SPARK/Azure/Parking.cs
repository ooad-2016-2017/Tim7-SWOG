using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARK.Azure
{
    class Parking
    {
        public string id
        {
            get;
            set;
        }
        public int id_vlasnika
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public int Capacity
        {
            get;
            set;
        }
        public double CoordX
        {
            get;
            set;
        }
        public double CoordY
        {
            get;
            set;
        }
        public double MinCredits
        {
            get;
            set;
        }
        public double MonthlyProfit
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int NumTakenSpaces
        {
            get;
            set;
        }
        public double Price
        {
            get;
            set;
        }
        public double TodaysProfit
        {
            get;
            set;
        }
        public int Zone
        {
            get;
            set;
        }
        public string WorkingFrom
        {
            get;
            set;
        }
        public string WorkingTo
        {
            get;
            set;
        }
    }
}
