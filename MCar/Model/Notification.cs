using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class Notification
    {
        #region ctor

        public Notification(Contract contract)
        {
            _contract = contract;
            Customer = contract.Customer;
            CreateContent();
        }

        #endregion

        #region Fields

        private readonly Contract _contract;

        #endregion

        #region Properties

        public Person Customer { get; set; }
        public string Content { get; set; }
        public double RestOfPaid { get; set; }
        public bool HasNotification { get; set; }

        #endregion

        #region Methods

        private bool HasPayment()
        {
            var today = DateTime.Now;

            RestOfPaid = 0;

            foreach (var payment in _contract.Payments.FindAll(p => ConvertStringToDateTime(p.Date) <= today))
            {
                RestOfPaid += payment.Rest;
            }

            return RestOfPaid != 0;
        }

        private DateTime ConvertStringToDateTime(string date)
        {
            int day = int.Parse(date.Substring(0, 2));
            int month = int.Parse(date.Substring(3, 2));
            int year = int.Parse(date.Substring(6));

            return new DateTime(year, month, day);
        }

        private void CreateContent()
        {
            if (HasPayment())
            {
                HasNotification = true;
                Content = $"adlı alıcı {RestOfPaid} miqdarında ödəniş etməlidir!";
            }
            else
            {
                HasNotification = false;
            }
        }

        #endregion

    }

}
