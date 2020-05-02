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
            var Bouquets = bouquetLogic.Read(null);
            var list = new List<ReportBouquetFlowerViewModel>();
            foreach (var bouquet in Bouquets)
            {
                foreach (var bf in bouquet.BouquetFlowers)
                {
                    var record = new ReportBouquetFlowerViewModel
                    {
                        BouquetName = bouquet.BouquetName,
                        FlowerName = bf.Value.Item1,
                        Count = bf.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                BouquetName = x.BouquetName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .GroupBy(x => x.DateCreate.Date)
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
                Title = "Список заказов",
                Orders = GetOrders(model)
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