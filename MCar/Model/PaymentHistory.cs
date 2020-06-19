using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class PaymentHistory
    {
        public Person Customer { get; set; }
        public DateTime PaymentTime { get; set; }
        public double Paid { get; set; }
    }
}
