using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCar.Model;
using MCar.Utils;

namespace MCar.Context
{
    public class DataContext
    {
        #region Ctor

        public DataContext()
        {
            Init();
        }

        #endregion

        #region Properties

        public List<Contract> Contracts { get; set; }
        public List<Car> Cars { get; set; }

        #endregion

        #region Methods

        private void Init()
        {
            Contracts = new List<Contract>(XmlHelper.GetContractList());
            Cars = new List<Car>(XmlHelper.GetCarList());

            Contracts = Contracts.OrderBy(c => c.ContractNumber).ToList();

        }


        #endregion

    }
}
