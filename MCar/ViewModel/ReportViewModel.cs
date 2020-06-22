using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MCar.Model;
using MCar.Utils;

namespace MCar.ViewModel
{
    public class ReportViewModel : BaseViewModel
    {
        #region ctor

        public ReportViewModel()
        {
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<Report> ReportList { get; set; }

        public ObservableCollection<Report> SelectedTimeReportList { get; set; }

        public ObservableCollection<Contract> ContractList { get; set; }

        public ObservableCollection<ContractType> ContractTypeList { get; set; } = new ObservableCollection<ContractType>
        {
            ContractType.All,
            ContractType.Actual,
            ContractType.Closed
        };

        private ContractType _selectedContractType;
        public ContractType SelectedContractType
        {
            get => _selectedContractType;
            set
            {
                Set(() => SelectedContractType, ref _selectedContractType, value);

                if (SelectedContractType == ContractType.All)
                {
                    FillAllReportList();
                    FillAllValues();
                }
                else if (SelectedContractType == ContractType.Actual)
                {
                    FillActiveReportList();
                    FillAllValues();
                }
                else
                {
                    FillClosedReportList();
                    FillAllValues();
                }
            }
        }

        private DateTime _selectedTime;
        public DateTime SelectedTime
        {
            get => _selectedTime;
            set
            {
                Set(() => SelectedTime, ref _selectedTime, value);
                FillSelectedTimeReportList();
                FillSelectedTimeAllValues();
            }
        }

        private double _allPaid;
        public double AllPaid
        {
            get => _allPaid;
            set => Set(() => AllPaid, ref _allPaid, value);
        }

        private double _allMustBePaid;
        public double AllMustBePaid
        {
            get => _allMustBePaid;
            set => Set(() => AllMustBePaid, ref _allMustBePaid, value);
        }

        private double _allRest;
        public double AllRest
        {
            get => _allRest;
            set => Set(() => AllRest, ref _allRest, value);
        }

        private double _allIncome;
        public double AllIncome
        {
            get => _allIncome;
            set => Set(() => AllIncome, ref _allIncome, value);
        }

        private double _allSelectedPaid;
        public double AllSelectedPaid
        {
            get => _allSelectedPaid;
            set => Set(() => AllSelectedPaid, ref _allSelectedPaid, value);
        }

        private double _allSelectedMustBePaid;
        public double AllSelectedMustBePaid
        {
            get => _allSelectedMustBePaid;
            set => Set(() => AllSelectedMustBePaid, ref _allSelectedMustBePaid, value);
        }

        private double _allSelectedRest;
        public double AllSelectedRest
        {
            get => _allSelectedRest;
            set => Set(() => AllSelectedRest, ref _allSelectedRest, value);
        }

        private double _allSelectedIncome;
        public double AllSelectedIncome
        {
            get => _allSelectedIncome;
            set => Set(() => AllSelectedIncome, ref _allSelectedIncome, value);
        }

        #endregion

        #region Methods

        private void Init()
        {
            ContractList = new ObservableCollection<Contract>(MainWindow.Data.Contracts);
            ReportList = new ObservableCollection<Report>();
            SelectedTimeReportList = new ObservableCollection<Report>();
            SelectedContractType = ContractType.Actual;
            SelectedTime = DateTime.Now;
        }

        private void FillAllReportList()
        {
            ReportList.Clear();

            foreach (var c in ContractList)
            {
                ReportList.Add(ConvertToReport(c));
            }
        }

        private void FillClosedReportList()
        {
            ReportList.Clear();

            foreach (var c in ContractList)
            {
                if (c.IsClose)
                {
                    ReportList.Add(ConvertToReport(c));
                }
            }
        }

        private void FillActiveReportList()
        {
            ReportList.Clear();

            foreach (var c in ContractList)
            {
                if (!c.IsClose)
                {
                    ReportList.Add(ConvertToReport(c));
                }
            }
        }

        private void FillSelectedTimeReportList()
        {
            SelectedTimeReportList.Clear();

            foreach (var contract in ContractList)
            {
                if (contract.Payments.FindAll(c => ConvertStringToDateTime(c.Date) <= SelectedTime).Count == 0)
                    continue;

                SelectedTimeReportList.Add(ConvertToSelectedTimeReport(contract));
            }
        }
        private void FillAllValues()
        {
            AllPaid = 0;
            AllRest = 0;
            AllIncome = 0;
            AllMustBePaid = 0;

            foreach (var c in ReportList)
            {
                AllPaid += c.SumOfPaid + c.InitialPayment;
                AllRest += c.SumOfRest;
                AllIncome += c.Income;
                AllMustBePaid += c.Car.SellPrice;
            }

            AllPaid = Math.Round(AllPaid, 2);
            AllRest = Math.Round(AllRest, 2);
            AllIncome = Math.Round(AllIncome, 2);
            AllMustBePaid = Math.Round(AllMustBePaid, 2);

        }

        private void FillSelectedTimeAllValues()
        {
            AllSelectedPaid = 0;
            AllSelectedRest = 0;
            AllSelectedIncome = 0;
            AllSelectedMustBePaid = 0;

            foreach (var c in SelectedTimeReportList)
            {
                AllSelectedPaid += c.SumOfPaid;
                AllSelectedRest += c.SumOfRest;
                AllSelectedIncome += c.Income;
                AllSelectedMustBePaid += c.SumOfMustBePaid;
            }

            AllSelectedPaid = Math.Round(AllSelectedPaid, 2);
            AllSelectedRest = Math.Round(AllSelectedRest, 2);
            AllSelectedIncome = Math.Round(AllSelectedIncome, 2);
            AllSelectedMustBePaid = Math.Round(AllSelectedMustBePaid, 2);

        }

        private Report ConvertToReport(Contract contract)
        {
            return new Report
            {
                ContractNumber = contract.ContractNumber,
                Customer = contract.Customer,
                Car = contract.Car,
                Income = contract.Income,
                InitialPayment = contract.InitialPayment,
                Payments = contract.Payments
            };
        }

        private Report ConvertToSelectedTimeReport(Contract contract)
        {

            return new Report
            {
                ContractNumber = contract.ContractNumber,
                Customer = contract.Customer,
                Car = contract.Car,
                Income = contract.Income,
                InitialPayment = contract.InitialPayment,
                Payments = contract.Payments.FindAll( c => ConvertStringToDateTime(c.Date) <= SelectedTime)
            };
        }

        private DateTime ConvertStringToDateTime(string date)
        {
            int day = int.Parse(date.Substring(0, 2));
            int month = int.Parse(date.Substring(3, 2));
            int year = int.Parse(date.Substring(6));

            return new DateTime(year, month, day);
        }

        #endregion
    }
}
