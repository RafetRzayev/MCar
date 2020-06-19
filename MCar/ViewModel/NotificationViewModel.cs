using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCar.Model;
using MCar.Utils;

namespace MCar.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Ctor

        public NotificationViewModel()
        {
            Init();
        }

        #endregion

        #region Properties

        public ObservableCollection<Notification> NotificationList { get; set; } =
            new ObservableCollection<Notification>();

        public ObservableCollection<Contract> ContractList { get; set; }


        #endregion

        #region Methods

        private void Init()
        {
            ContractList = new ObservableCollection<Contract>(XmlHelper.GetContractList());
            FillNotification();
        }

        private void FillNotification()
        {
            Notification n;

            foreach (var contract in ContractList)
            {
                n = new Notification(contract);

                if (!n.HasNotification)
                    continue;

                NotificationList.Add(n);
            }
        }

        #endregion
    }
}
