using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.HelperModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FlowerShopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IFlowerLogic flowerLogic;
        private readonly IBouquetLogic bouquetLogic;
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IBouquetLogic bouquetLogic, IFlowerLogic flowerLogic, IOrderLogic orderLogic)
        {
            this.bouquetLogic = bouquetLogic;
            this.flowerLogic = flowerLogic;
            this.orderLogic = orderLogic;
        }

        public List<ReportBouquetFlowerViewModel> GetBouquetFlower()
        {
            var Flowers = flowerLogic.Read(null);
            var Bouquets = bouquetLogic.Read(null);
            var list = new List<ReportBouquetFlowerViewModel>();
            foreach (var flower in Flowers)
            {
                foreach (var bouquet in Bouquets)
                {
                    if (bouquet.BouquetFlowers.ContainsKey(flower.Id))
                    {
                        var record = new ReportBouquetFlowerViewModel
                        {
                            BouquetName = bouquet.BouquetName,
                            FlowerName = flower.FlowerName,
                            Count = bouquet.BouquetFlowers[flower.Id].Item2
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders()
        {
            return orderLogic.Read(null)
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                BouquetName = x.BouquetName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveBouquetsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список букетов",
                Bouquets = bouquetLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение закусок с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Заказы",
                Orders = GetOrders()
            });
        }

        /// <summary>
        /// Сохранение закусок с продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveBouquetFlowersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список букетов по цветам",
                BouquetFlowers = GetBouquetFlower(),
            });
        }
    }
}