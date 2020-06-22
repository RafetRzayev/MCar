using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using GalaSoft.MvvmLight.Command;
using MCar.Model;
using MCar.Utils;

namespace MCar.ViewModel
{
    public class MediatorViewModel : BaseViewModel
    {
        #region Ctor

        public MediatorViewModel()
        {
            Init();
        }

        #endregion

        #region Properties
        private List<Contract> allContracts;
        public ObservableCollection<Contract> ContractList { get; set; }

        private string _searchMediator;
        public string SearchMediator
        {
            get => _searchMediator;
            set
            {
                Set(() => SearchMediator, ref _searchMediator, value);
            }
        }

        private double _sumMediatorMoney;

        public double SumMediatorMoney
        {
            get => _sumMediatorMoney;
            set => Set(() => SumMediatorMoney, ref _sumMediatorMoney, value);
        }
        #endregion

        #region Methods

        public void Init()
        {
            ContractList = new ObservableCollection<Contract>(MainWindow.Data.Contracts);
            allContracts = ContractList.ToList();
            allContracts.ForEach(c => SumMediatorMoney += c.Mediator.Money);
        }

        private void FillContractListDependentMediator()
        {
            ContractList.Clear();
            SumMediatorMoney = 0;
            string s = "";
            foreach (var c in allContracts)
            {
                s = "";

                if (string.IsNullOrEmpty(SearchMediator))
                    continue;

                if (!string.IsNullOrEmpty(c.Mediator.FirstName))
                    s += c.Mediator.FirstName;

                if (!string.IsNullOrEmpty(c.Mediator.LastName))
                    s += c.Mediator.LastName;

                if (!string.IsNullOrEmpty(c.Mediator.FatherName))
                    s += c.Mediator.FatherName;

                if (!string.IsNullOrEmpty(c.Mediator.Seriya))
                    s += c.Mediator.Seriya;

                if (s.Trim().IndexOf(SearchMediator.Replace(" ","").Trim(), StringComparison.OrdinalIgnoreCase) != -1)
                {
                    ContractList.Add(c);
                    SumMediatorMoney += c.Mediator.Money;
                }

            }
        }

        private void FillAllContract()
        {
            ContractList.Clear();
            SumMediatorMoney = 0;

            allContracts.ForEach(c =>
            {
                ContractList.Add(c);
                SumMediatorMoney += c.Mediator.Money;
            });

            SearchMediator = string.Empty;
        }

        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                       ?? (_searchCommand = new RelayCommand(
                           FillContractListDependentMediator, () => true));
            }
        }

        private RelayCommand _showAllCommand;
        public RelayCommand ShowAllCommand
        {
            get
            {
                return _showAllCommand
                       ?? (_showAllCommand = new RelayCommand(
                           FillAllContract, () => true));
            }
        }

        #endregion
    }
}
