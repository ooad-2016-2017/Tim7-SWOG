using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPARK.Model
{
    public class SPARK
    {
        protected List<Parking> parkingList;
        protected List<User> userList;
    //    protected PaymentManager paymentManager;

        protected bool reserveParking()
        {
            return true;
        }
        protected bool registerUser()
        {
            return true;
        }
        protected bool registerParking()
        {
            return true;
        }
        protected bool registerOwner()
        {
            return true;
        }
    }
}
