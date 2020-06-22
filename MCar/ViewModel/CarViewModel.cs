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
    public class CarViewModel : BaseViewModel
    {
        #region ctor

        public CarViewModel()
        {
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<Car> CarList { get; set; }

        private Car _selectedCar;

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                Set(() => SelectedCar, ref _selectedCar, value);
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => Set(() => Model, ref _model, value);
        }

        private string _carNumber;
        public string CarNumber
        {
            get => _carNumber;
            set => Set(() => CarNumber, ref _carNumber, value);
        }

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

        private Person _owner;
        public Person Owner
        {
            get => _owner;
            set => Set(() => Owner, ref _owner, value);
        }

        private double _buyPrice;
        public double BuyPrice
        {
            get => _buyPrice;
            set => Set(() => BuyPrice, ref _buyPrice, value);
        }

        private double _sellPrice;
        public double SellPrice
        {
            get => _sellPrice;
            set => Set(() => SellPrice, ref _sellPrice, value);
        }

        private int _madeYear;
        public int MadeYear
        {
            get => _madeYear;
            set => Set(() => MadeYear, ref _madeYear, value);
        }

        private string _notification;
        public string Notification
        {
            get => _notification;
            set => Set(() => Notification, ref _notification, value);
        }

        public SnackbarMessageQueue MyMessageQueue { get; set; } =
            new SnackbarMessageQueue(TimeSpan.FromMilliseconds(250));

        #endregion

        #region Methods

        private void Init()
        {
            CarList = new ObservableCollection<Car>(MainWindow.Data.Cars);
        }

        private void AddCar()
        {
            MessageBoxResult mbResult = MessageBox.Show("Təsdiqləyirsiz?", "Xəbərdarlıq",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (mbResult == MessageBoxResult.Cancel)
                return;

            Car car = new Car
            {
                Model = Model,
                CarNumber = CarNumber,
                Owner = new Person
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    FatherName = FatherName,
                    MobilNumber = MobilNumber,
                    Seriya = Seriya
                },
                BuyPrice = BuyPrice,
                SellPrice = SellPrice,
                MadeYear = MadeYear
            };

            CarList.Add(car);
           
            MainWindow.Data.Cars.Add(car);

            XmlHelper.SetCarList(CarList);

            MyMessageQueue.Enqueue("Əlavə edildi.");

        }

        private void RemoveCar()
        {
            if (SelectedCar != null)
            {
                MessageBoxResult mbResult = MessageBox.Show("Bu maşın haqqında məlumatları silməyə əminsiz?", "Xəbərdarlıq",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (mbResult == MessageBoxResult.Cancel)
                    return;

                CarList.Remove(SelectedCar);

                XmlHelper.SetCarList(CarList);

                MyMessageQueue.Enqueue("Silindi.");
            }
            else
            {
                MyMessageQueue.Enqueue("Maşın seçməlisiniz.");
            }

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
                               AddCar();
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
                               RemoveCar();
                           }, () => true));
            }
        }
        #endregion
    }
}
