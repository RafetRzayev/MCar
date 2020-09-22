using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCar.Model;

namespace MCar.ViewModel
{
    class AboutContractViewModel : BaseViewModel
    {
        #region Ctor

        public AboutContractViewModel(Contract contract)
        {
            Contract.Add(contract);
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<PaymentHistory> PaymentHistoryList { get; set; } =
            new ObservableCollection<PaymentHistory>();

        public ObservableCollection<Payment> PaymentList { get; set; } =
            new ObservableCollection<Payment>();

        public ObservableCollection<Contract> Contract { get; set; } = new ObservableCollection<Contract>();
       

        #endregion

        #region Methods

        private void Init()
        {
            PaymentHistoryList = new ObservableCollection<PaymentHistory>(Contract.FirstOrDefault().PaymentHistories);
            PaymentList=new ObservableCollection<Payment>(Contract.FirstOrDefault().Payments);
        }



        #endregion
    }
}
