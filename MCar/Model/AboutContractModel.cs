using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class AboutContractModel
    {
        #region Properties

        public string CustomerFullName { get; set; }
        public string MediatorFullName { get; set; }
        public string CarName { get; set; }
        public double InitialPayment { get; set; }
        public double PaymentPerMonth { get; set; }
        public int Term { get; set; }
        public double Income { get; set; }
        public string CreatingDate { get; set; }
        public double CarBuyPrice { get; set; }
        public double CarSellPrice { get; set; }
        public double MediatorMoney { get; set; }
        public string Status { get; set; }

        #endregion
    }
}
