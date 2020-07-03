using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Flower> Flowers { get; set; }

        public List<Order> Orders { get; set; }

        public List<Bouquet> Bouquets { get; set; }

        public List<BouquetFlower> BouquetFlowers { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StorageFlower> StorageFlowers { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { set; get; }

        public List<MessageInfo> MessageInfoes { get; set; }

        private DataListSingleton()
        {
            Flowers = new List<Flower>();
            Orders = new List<Order>();
            Bouquets = new List<Bouquet>();
            BouquetFlowers = new List<BouquetFlower>();
            Storages = new List<Storage>();
            StorageFlowers = new List<StorageFlower>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            MessageInfoes = new List<MessageInfo>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
