using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using MCar.Model;

namespace MCar.Utils
{
    public static class XmlHelper
    {
        public static List<Car> GetCarList()
        {
            XmlSerializer xmlSerializer=new XmlSerializer(typeof(List<Car>));
            List<Car> carList = new List<Car>();

            using (FileStream fs=new FileStream("data\\cars.xml",FileMode.OpenOrCreate))
            {
                try
                {
                    carList = (List<Car>)xmlSerializer.Deserialize(fs);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Masinlar haqqinda melumati fayldan oxuyan zaman problem yarandi.\n"+e.Message);
                }
            }

            return carList;
        }

        public static List<Contract> GetContractList()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract));
            List<Contract> contractList = new List<Contract>();
            Contract contract = new Contract();

            foreach (var contractFile in Directory.GetFiles("data\\contracts"))
            {
                using (FileStream fs = new FileStream(contractFile, FileMode.OpenOrCreate))
                {
                    try
                    {
                        contract = (Contract) xmlSerializer.Deserialize(fs);
                        contractList.Add(contract);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Muqavileler haqqinda melumati fayldan oxuyan zaman problem yarandi.\n" + e.Message);
                    }
                }
            }
           

            return contractList;
        }

        public static void SetCarList(ObservableCollection<Car> carList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Car>));

            using (FileStream fs = new FileStream("data\\cars.xml", FileMode.Create))
            {
                try
                {
                    xmlSerializer.Serialize(fs, carList);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Masinlar haqqinda melumati fayla daxil eden zaman problem yarandi.\n"+e.Message);
                }
            }

        }

        public static void AddContract(Contract contract)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract));

            using (FileStream fs = new FileStream(Path.Combine("data\\contracts",contract.Id+".xml"), FileMode.Create))
            {
                try
                {
                    xmlSerializer.Serialize(fs, contract);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Muqavile haqqinda melumati fayla daxil eden zaman problem yarandi.\n" + e.Message);
                }
            }

        }

        public static void RemoveContract(Contract contract)
        {
            File.Delete(Path.Combine("data\\contracts", contract.Id + ".xml"));
        }

    }
}
