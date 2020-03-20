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
        public ReportLogic(IBouquetLogic productLogic, IFlowerLogic componentLogic, IOrderLogic orderLLogic)
        {
            this.bouquetLogic = productLogic;
            this.flowerLogic = componentLogic;
            this.orderLogic = orderLLogic;
        }

        // Получение списка компонент с указанием, в каких изделиях используются
        public List<ReportBouquetFlowerViewModel> GetProductComponent()
        {
            var components = flowerLogic.Read(null);
            var products = bouquetLogic.Read(null);
            var list = new List<ReportBouquetFlowerViewModel>();
            foreach (var component in components)
            {
                var record = new ReportBouquetFlowerViewModel
                {
                    FlowerName = component.FlowerName,
                    Bouquets = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var product in products)
                {
                    if (product.BouquetFlowers.ContainsKey(component.Id))
                    {
                        record.Bouquets.Add(new Tuple<string, int>(product.BouquetName, product.BouquetFlowers[component.Id].Item2));
                        record.TotalCount += product.BouquetFlowers[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        // Получение списка заказов за определенный период
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

        // Сохранение компонент в файл-Word
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Flowers = flowerLogic.Read(null)
            });
        }

        // Сохранение компонент с указаеним продуктов в файл-Excel
        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                BouquetFlowers = GetProductComponent()
            });
        }

        // Сохранение заказов в файл-Pdf
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}