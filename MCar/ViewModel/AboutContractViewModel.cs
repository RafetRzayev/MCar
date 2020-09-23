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
            Init(contract);
        }

        #endregion

        #region Properties

        public ObservableCollection<PaymentHistory> PaymentHistoryList { get; set; } =
            new ObservableCollection<PaymentHistory>();

        public ObservableCollection<Payment> PaymentList { get; set; } =
            new ObservableCollection<Payment>();

        private AboutContractModel _aboutContract;
        public AboutContractModel AboutContract
        {
            get => _aboutContract;
            set => Set(() => AboutContract, ref _aboutContract, value);
        }

        #endregion

        #region Methods

        private void Init(Contract contract)
        {
            AboutContract = new AboutContractModel
            {
                CustomerFullName = contract.Customer.ToString(),
                MediatorFullName = contract.Mediator.ToString(),
                CreatingDate = contract.CreatingDate,
                Term = contract.Term,
                CarName = contract.Car.ToString(),
                CarBuyPrice = contract.Car.BuyPrice,
                CarSellPrice = contract.Car.SellPrice,
                InitialPayment = contract.InitialPayment,
                PaymentPerMonth = contract.PaymentPerMonth,
                MediatorMoney = contract.Mediator.Money,
                Income = contract.Income,
                Status = contract.Status
            };
            PaymentHistoryList = new ObservableCollection<PaymentHistory>(contract.PaymentHistories);
            PaymentList = new ObservableCollection<Payment>(contract.Payments);
        }



        #endregion
    }
}
