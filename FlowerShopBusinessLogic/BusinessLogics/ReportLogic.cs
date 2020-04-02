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
        private readonly IFlowerLogic FlowerLogic;
        private readonly IBouquetLogic BouquetLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IBouquetLogic BouquetLogic, IFlowerLogic FlowerLogic,
       IOrderLogic orderLogic)
        {
            this.BouquetLogic = BouquetLogic;
            this.FlowerLogic = FlowerLogic;
            this.orderLogic = orderLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportBouquetFlowerViewModel> GetSnackFood()
        {
            var Flowers = FlowerLogic.Read(null);
            var Bouquets = BouquetLogic.Read(null);
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
                            Count = bouquet.BouquetFlowers[bouquet.Id].Item2
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
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
                Title = "Список закусок",
                Bouquets = BouquetLogic.Read(null)
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
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
        /// <summary>
        /// Сохранение закусок с продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveBouquetsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список закусок по продуктам",
                BouquetFlowers = GetSnackFood(),
            });
        }
    }
}