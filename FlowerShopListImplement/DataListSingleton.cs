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

        public List<Client> Clients { set; get; }

        private DataListSingleton()
        {
            Flowers = new List<Flower>();
            Orders = new List<Order>();
            Bouquets = new List<Bouquet>();
            BouquetFlowers = new List<BouquetFlower>();
            Clients = new List<Client>();
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
