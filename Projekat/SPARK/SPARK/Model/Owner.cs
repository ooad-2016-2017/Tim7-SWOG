using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPARK.Model
{
    public class Owner : User
    {
        List<Parking> parkingList;
        public bool registerParking()
        {
            return true;
        }
        public bool editParking()
        {
            return true;
        }
        public bool deleteParking()
        {
            return true;
        }

    }
}
