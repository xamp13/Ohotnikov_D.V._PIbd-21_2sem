using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.HelperModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IFlowerLogic FlowerLogic;
        private readonly IBouquetLogic BouquetLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic StorageLogic;
        public ReportLogic(IBouquetLogic BouquetLogic, IFlowerLogic FlowerLogic,
       IOrderLogic orderLLogic, IStorageLogic storageLogic)
        {
            this.BouquetLogic = BouquetLogic;
            this.FlowerLogic = FlowerLogic;
            this.orderLogic = orderLLogic;
            this.StorageLogic = storageLogic;
        }

        public List<ReportBouquetFlowerViewModel> GetBouquetFlower()
        {
            var Flowers = FlowerLogic.Read(null);
            var Bouquets = BouquetLogic.Read(null);
            var list = new List<ReportBouquetFlowerViewModel>();
            foreach (var bouquet in Bouquets)
            {
                var record = new ReportBouquetFlowerViewModel
                {
                    BouquetName = bouquet.BouquetName,
                    Flowers = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var flower in Flowers)
                {
                    if (bouquet.BouquetFlowers.ContainsKey(flower.Id))
                    {
                        record.Flowers.Add(new Tuple<string, int>(flower.FlowerName,
                       bouquet.BouquetFlowers[flower.Id].Item2));
                        record.TotalCount +=
                       bouquet.BouquetFlowers[flower.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        public List<ReportStorageFlowerViewModel> GetStorageFlower()
        {
            var flowers = FlowerLogic.Read(null);
            var storages = StorageLogic.GetList();
            var list = new List<ReportStorageFlowerViewModel>();
            foreach (var storage in storages)
            {
                var record = new ReportStorageFlowerViewModel
                {
                    StorageName = storage.StorageName,
                    Flowers = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var flower in flowers)
                {
                    var storageFlowers = storage.StorageFlowers.Find(x => x.FlowerId == flower.Id);
                    if (storageFlowers != null)
                    {
                        record.Flowers.Add(new Tuple<string, int>(flower.FlowerName,
                       storageFlowers.Count));
                        record.TotalCount += storageFlowers.Count;
                    }
                }
                list.Add(record);
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
        public List<ReportFlowersViewModel> GetFlowers()
        {
            List<ReportFlowersViewModel> result = new List<ReportFlowersViewModel>();
            var storages = StorageLogic.GetList();
            foreach (var storage in storages)
            {
                var storageFlowers = storage.StorageFlowers;
                foreach (var sf in storageFlowers)
                {
                    result.Add(new ReportFlowersViewModel
                    {
                        StorageName = storage.StorageName,
                        FlowerName = FlowerLogic.Read(new FlowerBindingModel
                        {
                            Id = sf.FlowerId
                        })[0].FlowerName,
                        Count = sf.Count
                    });
                }
            }
            return result.OrderBy(x => x.FlowerName).ToList();
        }
        public void SaveFlowersToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список букетов",
                Bouquets = BouquetLogic.Read(null)
            });

        }
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Storages = StorageLogic.GetList()
            });
        }
        public void SaveStorageFlowerToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                StorageFlowers = GetStorageFlower()
            });
        }
        public void SaveBouquetFlowerToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список букетов",
                BouquetFlowers = GetBouquetFlower()
            });
        }
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
        public void SaveFlowersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список цветов",
                Flowers = GetFlowers()
            });
        }
    }
}