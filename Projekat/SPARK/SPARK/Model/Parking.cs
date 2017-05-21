using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPARK.Model
{
    public class Parking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        protected int id;
        protected string address;
        protected string name;
        protected int zone;
        protected double price;
        protected double coordX;
        protected double coordY;
        protected int capacity;
        protected int numTakenSpaces;
        protected KeyValuePair<DateTime, DateTime> workingHours;
        protected double todaysProfit;
        protected double monthlyProfit;
        protected static int numParkingSpaces;
        protected int minCredits;
        protected Dictionary<int, DateTime> reservations;

        public Parking() { }

        private bool resetToday() { return true; }
        private bool resetMonth() { return true; }

        public bool takeParkingSpace()
        {
            numTakenSpaces++;
            return true;
        }
        public bool freeParkingSpace()
        {
            numTakenSpaces--;
            return true;
        }
        public bool getState()
        {
            return numTakenSpaces < capacity;
        }
    }
}
