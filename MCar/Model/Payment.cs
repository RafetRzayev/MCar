using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class Payment
    {
        public string Date { get; set; }
        public double MustBePaid { get; set; }
        public double Paid { get; set; }
        public double Rest => MustBePaid - Paid;
        public bool IsPaid => Rest == 0;
    }
}
