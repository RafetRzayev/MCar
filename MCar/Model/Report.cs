using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class Report : Contract
    {
        public double SumOfPaid
        {
            get => Math.Round(Payments.Sum(payment => payment.Paid), 2);
        }

        public double SumOfMustBePaid
        {
            get => Math.Round(Payments.Sum(payment => payment.MustBePaid), 2);
        }

        public double SumOfRest
        {
            get => Math.Round(Payments.Sum(payment => payment.Rest), 2);
        }
    }
}
