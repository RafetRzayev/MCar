using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    [Serializable]
    public class Car
    {
        public string Model { get; set; }
        public string CarNumber { get; set; }
        public Person Owner { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public int MadeYear { get; set; }
        public Status Status { get; set; }
        public override string ToString()
        {
            return $"{Model} {CarNumber}({MadeYear})";
        }
    }

    public enum Status
    {
        [Description("Aktiv")]
        Active,
        [Description("Passiv")]
        Passive
    }
}
