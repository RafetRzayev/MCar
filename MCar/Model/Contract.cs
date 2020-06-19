using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    [Serializable]
    public class Contract
    {
        public string Id { get; set; }
        public int ContractNumber { get; set; }
        public Person Customer { get; set; }
        public Mediator Mediator { get; set; }
        public Car Car { get; set; }
        public double InitialPayment { get; set; }
        public double PaymentPerMonth { get; set; }
        public int Term { get; set; }
        public double Income { get; set; }
        public string CreatingDate { get; set; }
        public List<Payment> Payments { get; set; }
        public List<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();
        public bool IsClose
        {
            get => Payments.Sum(p => p.Rest) == 0;
        }

        public string Status => IsClose ? "Bağlı" : "Açıq";
    }

    public enum ContractType
    {
        [Description("Bütün")]
        All,
        [Description("Cari")]
        Actual,
        [Description("Bağlanmış")]
        Closed
    }

}
