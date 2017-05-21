using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARK.Model
{
    public class PaymentManager
    {
        private List<PaymentMethod> paymentMethods;

        public List<PaymentMethod> PaymentMethods
        {
            get
            {
                return paymentMethods;
            }

            set
            {
                paymentMethods = value;
            }
        }
    }
}
