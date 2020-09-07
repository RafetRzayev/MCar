using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using MCar.Model;
using MCar.Utils;

namespace MCar.ViewModel
{
    class ContractViewModel : BaseViewModel
    {

        #region ctor

        public ContractViewModel()
        {
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<Car> FullCarList { get; set; }

        public ObservableCollection<Car> CarList { get; set; }

        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                Set(() => SelectedCar, ref _selectedCar, value);

                if (SelectedCar != null)
                {
                    SellPrice = SelectedCar.SellPrice;
                    Income = SelectedCar.SellPrice - SelectedCar.BuyPrice - MediatorMoney;
                    CreatingDate = DateTime.Now;
                }

            }
        }

        public ObservableCollection<Contract> ContractList { get; set; }

        private Contract _selectedContract;
        public Contract SelectedContract
        {
            get => _selectedContract;
            set
            {
                Set(() => SelectedContract, ref _selectedContract, value);
                FillSelectedContractPaymentList();
            }
        }

        public ObservableCollection<Payment> SelectedContractPaymentList { get; set; } =
            new ObservableCollection<Payment>();

        private Payment _selectedContractPayment;
        public Payment SelectedContractPayment
        {
            get => _selectedContractPayment;
            set
            {
                Set(() => SelectedContractPayment, ref _selectedContractPayment, value);
              
                if (SelectedContractPayment != null)
                {
                    RestPaid = SelectedContractPayment.Rest;
                    Paid = 0;
                }
            }
        }
        public List<Payment> PaymentList { get; set; } = new List<Payment>();

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(() => FirstName, ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => Set(() => LastName, ref _lastName, value);
        }

        private string _fatherName;
        public string FatherName
        {
            get => _fatherName;
            set => Set(() => FatherName, ref _fatherName, value);
        }

        private string _mobilNumber;
        public string MobilNumber
        {
            get => _mobilNumber;
            set => Set(() => MobilNumber, ref _mobilNumber, value);
        }

        private string _seriya;
        public string Seriya
        {
            get => _seriya;
            set => Set(() => Seriya, ref _seriya, value);
        }

        private string _mediatorFirstName;
        public string MediatorFirstName
        {
            get => _mediatorFirstName;
            set => Set(() => MediatorFirstName, ref _mediatorFirstName, value);
        }

        private string _mediatorLastName;
        public string MediatorLastName
        {
            get => _mediatorLastName;
            set => Set(() => MediatorLastName, ref _mediatorLastName, value);
        }

        private string _mediatorFatherName;
        public string MediatorFatherName
        {
            get => _mediatorFatherName;
            set => Set(() => MediatorFatherName, ref _mediatorFatherName, value);
        }

        private string _mediatorMobilNumber;
        public string MediatorMobilNumber
        {
            get => _mediatorMobilNumber;
            set => Set(() => MediatorMobilNumber, ref _mediatorMobilNumber, value);
        }

        private string _mediatorSeriya;
        public string MediatorSeriya
        {
            get => _mediatorSeriya;
            set => Set(() => MediatorSeriya, ref _mediatorSeriya, value);
        }

        private double _mediatorMoney;
        public double MediatorMoney
        {
            get => _mediatorMoney;
            set => Set(() => MediatorMoney, ref _mediatorMoney, value);
        }

        private double _sellPrice;
        public double SellPrice
        {
            get => _sellPrice;
            set => Set(() => SellPrice, ref _sellPrice, value);
        }

        private int _term;
        public int Term
        {
            get => _term;
            set
            {
                Set(() => Term, ref _term, value);
                PaymentPerMonth = Math.Round((SelectedCar.SellPrice - InitialPayment) / Term, 2);
            }
        }

        private double _initialPayment;
        public double InitialPayment
        {
            get => _initialPayment;
            set => Set(() => InitialPayment, ref _initialPayment, value);
        }

        private double _paymentPerMonth;
        public double PaymentPerMonth
        {
            get => _paymentPerMonth;
            set => Set(() => PaymentPerMonth, ref _paymentPerMonth, value);

        }

        private double _income;
        public double Income
        {
            get => _income;
            set => Set(() => Income, ref _income, value);
        }

        private double _restPaid;
        public double RestPaid
        {
            get => _restPaid;
            set => Set(() => RestPaid, ref _restPaid, value);
        }

        private double _paid;
        public double Paid
        {
            get => _paid;
            set => Set(() => Paid, ref _paid, value);
        }

        private DateTime _creatingDate;
        public DateTime CreatingDate
        {
            get => _creatingDate;
            set => Set(() => CreatingDate, ref _creatingDate, value);
        }

        public SnackbarMessageQueue MyMessageQueue { get; set; } =
            new SnackbarMessageQueue(TimeSpan.FromMilliseconds(250));


        #region Search properties

        private string _searchCustomer;
        public string SearchCustomer
        {
            get => _searchCustomer;
            set => Set(() => SearchCustomer, ref _searchCustomer, value);
        }

        private string _searchMediator;
        public string SearchMediator
        {
            get => _searchMediator;
            set => Set(() => SearchMediator, ref _searchMediator, value);
        }

        private string _searchCarModel;
        public string SearchCarModel
        {
            get => _searchCarModel;
            set => Set(() => SearchCarModel, ref _searchCarModel, value);
        }

        private string _searchCarNumber;
        public string SearchCarNumber
        {
            get => _searchCarNumber;
            set => Set(() => SearchCarNumber, ref _searchCarNumber, value);
        }

        #endregion



        #endregion

        #region Methods

        private void Init()
        {
            FullCarList = new ObservableCollection<Car>(MainWindow.Data.Cars);
            FillActiveCarList();
            ContractList = new ObservableCollection<Contract>(MainWindow.Data.Contracts);
        }

        private void FillActiveCarList()
        {
            List<Car> activeCars = new List<Car>();

            foreach (var car in FullCarList)
            {
                if (car.Status == Status.Active)
                {
                    activeCars.Add(car);
                }
            }

            CarList = new ObservableCollection<Car>(activeCars);
        }

        private void AddContract()
        {
            if (SelectedCar != null)
            {
                MessageBoxResult mbResult = MessageBox.Show("Müqaviləni təsdiqləyirsiz?", "Xəbərdarlıq",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (mbResult == MessageBoxResult.Cancel)
                    return;

                FillPayment();

                var d = DateTime.Now;

                SelectedCar.Status = Status.Passive;

                Contract contract = new Contract
                {
                    Id = $"{Seriya}{d.Second}{d.Minute}{d.Hour}{d.Day}{d.Month}{d.Year}",
                    ContractNumber = ContractList.Count+1,
                    Customer = new Person
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        FatherName = FatherName,
                        Seriya = Seriya,
                        MobilNumber = MobilNumber
                    },
                    Mediator = new Mediator
                    {
                        FirstName = MediatorFirstName,
                        LastName = MediatorLastName,
                        FatherName = MediatorFatherName,
                        MobilNumber = MediatorMobilNumber,
                        Money = MediatorMoney,
                        Seriya = MediatorSeriya
                    },
                    Car = SelectedCar,
                    InitialPayment = InitialPayment,
                    CreatingDate = CreatingDate.ToString(),
                    Term = Term,
                    PaymentPerMonth = PaymentPerMonth,
                    Income = Income,
                    Payments = PaymentList
                };

                XmlHelper.AddContract(contract);

                ContractList.Add(contract);

                MainWindow.Data.Contracts.Add(contract);

                XmlHelper.SetCarList(FullCarList);

                MyMessageQueue.Enqueue("Əlavə edildi.");
            }
            else
            {
                MyMessageQueue.Enqueue("Maşın seçməlisiniz!");
            }

        }

        private void PayContract()
        {
            if (SelectedContractPayment != null && SelectedContract != null)
            {
                MessageBoxResult mbResult = MessageBox.Show("Ödənişi təsdiqləyirsiz?", "Xəbərdarlıq", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (mbResult == MessageBoxResult.OK)
                {
                    SelectedContractPayment.Paid += Paid;

                    SelectedContract.PaymentHistories.Add(new PaymentHistory
                    {
                        Customer = SelectedContract.Customer,
                        PaymentTime = DateTime.Now,
                        Paid = Paid
                    });

                    XmlHelper.AddContract(SelectedContract);

                    Paid = 0;
                    RestPaid = SelectedContractPayment.Rest;

                    MyMessageQueue.Enqueue("Ödəniş təsdiqləndi");
                }

            }
            else
            {
                MyMessageQueue.Enqueue("Müqavilə seçməlisiniz");
            }

        }

        private void RemoveContract()
        {
            if (SelectedContract != null)
            {
                MessageBoxResult mbResult = MessageBox.Show("Silmək istədiyinizə əminsiz?", "Xəbərdarlıq", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (mbResult == MessageBoxResult.OK)
                {
                    XmlHelper.RemoveContract(SelectedContract);

                    MainWindow.Data.Contracts.Remove(SelectedContract);

                    ContractList.Remove(SelectedContract);

                    MyMessageQueue.Enqueue("Silindi!");
                }

            }
            else
            {
                MyMessageQueue.Enqueue("Müqavilə seçməlisiniz!");
            }

        }

        private void FillPayment()
        {
            PaymentList.Clear();

            for (int i = 0; i < Term; i++)
            {
                PaymentList.Add(new Payment
                {
                    MustBePaid = PaymentPerMonth,
                    Paid = 0,
                    Date = CreatingDate.AddMonths(i + 1).ToString()
                });
            }
        }

        private void FillSelectedContractPaymentList()
        {
            if (SelectedContract != null)
            {
                SelectedContractPaymentList.Clear();

                foreach (var payment in SelectedContract.Payments)
                {
                    if (!payment.IsPaid)
                        SelectedContractPaymentList.Add(payment);
                }
            }

        }
       
        private void Search()
        {
            ContractList.Clear();

            foreach (var c in MainWindow.Data.Contracts)
            {

                if (!string.IsNullOrEmpty(SearchCustomer))
                {
                    if (c.Customer.ToString().Replace(" ","")
                            .IndexOf(SearchCustomer.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) == -1)
                        continue;
                }

                if (!string.IsNullOrEmpty(SearchMediator))
                {
                    if (c.Mediator.ToString().Trim()
                            .IndexOf(SearchMediator.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) == -1)
                        continue;
                }

                if (!string.IsNullOrEmpty(SearchCarModel))
                {
                    if(string.IsNullOrEmpty(c.Car.Model))
                        continue;
                    
                    if (c.Car.Model.Replace(" ","")
                            .IndexOf(SearchCarModel.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) == -1)
                        continue;
                }

                if (!string.IsNullOrEmpty(SearchCarNumber))
                {
                    if(string.IsNullOrEmpty(c.Car.CarNumber))
                        continue;

                    if (c.Car.CarNumber.Replace(" ", "")
                            .IndexOf(SearchCarNumber.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) == -1)
                        continue;
                }

                ContractList.Add(c);

            }
        }


        private void FillAllContract()
        {
            ContractList.Clear();

            MainWindow.Data.Contracts.ForEach(c => ContractList.Add(c));

            SearchCustomer = string.Empty;
            SearchMediator = string.Empty;
            SearchCarModel = string.Empty;
            SearchCarNumber = string.Empty;
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand
                       ?? (_addCommand = new RelayCommand(
                           () =>
                           {
                               AddContract();
                           }, () => true));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand
                       ?? (_removeCommand = new RelayCommand(
                           () =>
                           {
                               RemoveContract();
                           }, () => true));
            }
        }

        private RelayCommand _payCommand;
        public RelayCommand PayCommand
        {
            get
            {
                return _payCommand
                       ?? (_payCommand = new RelayCommand(
                           PayContract, () => true));
            }
        }

        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                       ?? (_searchCommand = new RelayCommand(
                           () => Search(), () => true));
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