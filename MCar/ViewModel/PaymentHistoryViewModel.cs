using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using MCar.Model;
using MCar.Utils;

namespace MCar.ViewModel
{
    class PaymentHistoryViewModel : BaseViewModel
    {
        #region Ctor

        public PaymentHistoryViewModel()
        {
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<PaymentHistoryModel> PaymentHistoryList { get; set; } 
            = new ObservableCollection<PaymentHistoryModel>();

        public ObservableCollection<PaymentHistory> AllPaymentHistoryList { get; set; } =
            new ObservableCollection<PaymentHistory>();

        public ObservableCollection<Contract> ContractList { get; set; } 

        private double _allPaid;
        public double AllPaid
        {
            get => _allPaid;
            set => Set(() => AllPaid, ref _allPaid, value);
        }

        private DateTime _fromTime;
        public DateTime FromTime
        {
            get => _fromTime;
            set => Set(() => FromTime, ref _fromTime, value);
        }

        private DateTime _toTime;
        public DateTime ToTime
        {
            get => _toTime;
            set => Set(() => ToTime, ref _toTime, value);
        }

        private int _count;

        public int Count
        {
            get => _count;
            set => Set(() => Count, ref _count, value);
        }
        #endregion

        #region Methods

        public void Init()
        {
            ContractList = new ObservableCollection<Contract>(MainWindow.Data.Contracts);
            FillPaymentHistoryList();
            FillAllPaymentHistoryList();
            FromTime = DateTime.Now;
            ToTime = DateTime.Now;
        }

        private void FillPaymentHistoryList()
        {
            PaymentHistoryList.Clear();

            foreach (var contract in ContractList)
            {
                if (contract.PaymentHistories.Count == 0)
                    continue;

                PaymentHistoryList.Add(new PaymentHistoryModel 
                { 
                    Header = $"{contract.ContractNumber} -- {contract.Customer}", 
                    PaymentHistories = contract.PaymentHistories 
                });
            }
        }

        private void FillAllPaymentHistoryList()
        {
            AllPaymentHistoryList.Clear();
            AllPaid = 0;

            foreach (var contract in ContractList)
            {
                if (contract.PaymentHistories.Count == 0)
                    continue;

                foreach (var paymentHistory in contract.PaymentHistories)
                {
                    if (paymentHistory.PaymentTime < FromTime)
                        continue;

                    if (paymentHistory.PaymentTime > ToTime)
                        continue;

                    AllPaymentHistoryList.Add(paymentHistory);
                    AllPaid += paymentHistory.Paid;
                }
            }

            Count = AllPaymentHistoryList.Count;
        }

        private void FillPaymentHistoryListBetweenTimeInterval()
        {
            AllPaymentHistoryList.Clear();
            AllPaid = 0;

            foreach (var contract in ContractList)
            {
                if (contract.PaymentHistories.Count == 0)
                    continue;

                foreach (var paymentHistory in contract.PaymentHistories)
                {
                    if (CompareTime(paymentHistory.PaymentTime, FromTime) < 0)
                        continue;

                    if (CompareTime(paymentHistory.PaymentTime, ToTime) > 0)
                        continue;

                    AllPaymentHistoryList.Add(paymentHistory);
                    AllPaid += paymentHistory.Paid;
                }

            }

            Count = AllPaymentHistoryList.Count;
        }

        private int CompareTime(DateTime t1, DateTime t2)
        {

            if (t1.Year < t2.Year)
            {
                return -1;
            }
            else if (t1.Year > t2.Year) 
            {
                return 1;
            }
            else
            {
                if (t1.DayOfYear < t2.DayOfYear)
                    return -1;
                else if (t1.DayOfYear > t2.DayOfYear)
                    return 1;
                else
                {
                    return 0;
                }
            }
        }

        private RelayCommand _showCommand;
        public RelayCommand ShowCommand
        {
            get
            {
                return _showCommand
                       ?? (_showCommand = new RelayCommand(
                           () =>
                           {
                               FillPaymentHistoryListBetweenTimeInterval();
                           }, () => true));
            }
        }

        #endregion
    }
}
