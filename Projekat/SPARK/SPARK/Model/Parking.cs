using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPARK.Model
{
    public class Parking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int id;
        private string address;
        private string name;
        private int zone;
        private double price;
        private double coordX;
        private double coordY;
        private int capacity;
        private int numTakenSpaces;
        private KeyValuePair<DateTime, DateTime> workingHours;
        private double todaysProfit;
        private double monthlyProfit;
        private static int numParkingSpaces;
        private int minCredits;
        private Dictionary<int, DateTime> reservations;

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

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                if (value != "") address = value;
                else throw new Exception("Nije unesena adresa parkinga");
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
                else throw new Exception("Nije unesen naziv parkinga");
            }
        }

        public int Zone
        {
            get
            {
                return zone;
            }

            set
            {
                if (value > 0 && value < 5) zone = value;
                else throw new Exception("Nepravilno unesena zona parkinga");
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value >= 0) price = value;
                else throw new Exception("Nepravilno unesena cijena parkinga");
            }
        }

        public double CoordX
        {
            get
            {
                return coordX;
            }

            set
            {
                coordX = value;
            }
        }

        public double CoordY
        {
            get
            {
                return coordY;
            }

            set
            {
                coordY = value;
            }
        }

        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                capacity = value;
            }
        }

        public int NumTakenSpaces
        {
            get
            {
                return numTakenSpaces;
            }

            set
            {
                numTakenSpaces = value;
            }
        }

        public KeyValuePair<DateTime, DateTime> WorkingHours
        {
            get
            {
                return workingHours;
            }

            set
            {
                workingHours = value;
            }
        }

        public double TodaysProfit
        {
            get
            {
                return todaysProfit;
            }

            set
            {
                todaysProfit = value;
            }
        }

        public double MonthlyProfit
        {
            get
            {
                return monthlyProfit;
            }

            set
            {
                monthlyProfit = value;
            }
        }

        public static int NumParkingSpaces
        {
            get
            {
                return numParkingSpaces;
            }

            set
            {
                numParkingSpaces = value;
            }
        }

        public int MinCredits
        {
            get
            {
                return minCredits;
            }

            set
            {
                minCredits = value;
            }
        }

        public Dictionary<int, DateTime> Reservations
        {
            get
            {
                return reservations;
            }

            set
            {
                reservations = value;
            }
        }

        public Parking() { }

        private bool resetToday() { return true; }
        private bool resetMonth() { return true; }

        public bool takeParkingSpace()
        {
            NumTakenSpaces++;
            return true;
        }
        public bool freeParkingSpace()
        {
            NumTakenSpaces--;
            return true;
        }
        public bool getState()
        {
            return NumTakenSpaces < Capacity;
        }
    }
}
