using FlowerShopBusinessLogic.Enums;
using FlowerShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FlowerShopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string FlowerFileName = "Flower.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string BouquetFileName = "Bouquet.xml";

        private readonly string BouquetFlowerFileName = "BouquetFlower.xml";

        public List<Flower> Flowers { get; set; }

        public List<Order> Orders { get; set; }


        public List<Bouquet> Bouquets { get; set; }

        public List<BouquetFlower> BouquetFlowers { get; set; }

        private FileDataListSingleton()
        {
            Flowers = LoadFlowers();
            Orders = LoadOrders();
            Bouquets = LoadBouquets();
            BouquetFlowers = LoadBouquetFlowers();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveFlowers();
            SaveOrders();
            SaveBouquets();
            SaveBouquetFlowers();
        }

        private List<Flower> LoadFlowers()
        {
            var list = new List<Flower>();
            if (File.Exists(FlowerFileName))
            {
                XDocument xDocument = XDocument.Load(FlowerFileName);
                var xElements = xDocument.Root.Elements("Flower").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Flower
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FlowerName = elem.Element("FlowerName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        BouquetId = Convert.ToInt32(elem.Element("BouquetId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }

        private List<Bouquet> LoadBouquets()
        {
            var list = new List<Bouquet>();
            if (File.Exists(BouquetFileName))
            {
                XDocument xDocument = XDocument.Load(BouquetFileName);
                var xElements = xDocument.Root.Elements("Bouquet").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Bouquet
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        BouquetName = elem.Element("BouquetName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }

        private List<BouquetFlower> LoadBouquetFlowers()
        {
            var list = new List<BouquetFlower>();
            if (File.Exists(BouquetFlowerFileName))
            {
                XDocument xDocument = XDocument.Load(BouquetFlowerFileName);
                var xElements = xDocument.Root.Elements("BouquetFlower").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new BouquetFlower
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        BouquetId = Convert.ToInt32(elem.Element("BouquetId").Value),
                        FlowerId = Convert.ToInt32(elem.Element("FlowerId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }

        private void SaveFlowers()
        {
            if (Flowers != null)
            {
                var xElement = new XElement("Flowers");
                foreach (var Flower in Flowers)
                {
                    xElement.Add(new XElement("Flower",
                    new XAttribute("Id", Flower.Id),
                    new XElement("FlowerName", Flower.FlowerName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(FlowerFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("BouquetId", order.BouquetId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveBouquets()
        {
            if (Bouquets != null)
            {
                var xElement = new XElement("Bouquets");
                foreach (var Bouquet in Bouquets)
                {
                    xElement.Add(new XElement("Bouquet",
                    new XAttribute("Id", Bouquet.Id),
                    new XElement("BouquetName", Bouquet.BouquetName),
                    new XElement("Price", Bouquet.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(BouquetFileName);
            }
        }

        private void SaveBouquetFlowers()
        {
            if (BouquetFlowers != null)
            {
                var xElement = new XElement("BouquetFlowers");
                foreach (var BouquetFlower in BouquetFlowers)
                {
                    xElement.Add(new XElement("BouquetFlower",
                    new XAttribute("Id", BouquetFlower.Id),
                    new XElement("BouquetId", BouquetFlower.BouquetId),
                    new XElement("FlowerId", BouquetFlower.FlowerId),
                    new XElement("Count", BouquetFlower.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(BouquetFlowerFileName);
            }
        }
    }
}