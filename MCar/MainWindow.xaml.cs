using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCar.Context;
using MCar.ViewModel;

namespace MCar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetTimeFormat();

            Data = new DataContext();
        }

        public static  DataContext  Data { get; set; }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;

            switch (listView.SelectedIndex)
            {
                case 0:
                    DataContext = new CarViewModel();
                    break;
                case 1:
                    DataContext = new ContractViewModel();
                    break;
                case 2:
                    DataContext = new MediatorViewModel();
                    break;
                case 3:
                    DataContext = new ReportViewModel();
                    break;
                case 4:
                    DataContext = new NotificationViewModel();
                    break;
                case 5:
                    DataContext = new PaymentHistoryViewModel();
                    break;
            }
        }

        private void SetTimeFormat()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
