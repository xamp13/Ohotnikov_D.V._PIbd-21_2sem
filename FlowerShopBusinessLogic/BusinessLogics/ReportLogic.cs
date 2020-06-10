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
        private readonly IFlowerLogic flowerLogic;
        private readonly IBouquetLogic bouquetLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic storageLogic;
        public ReportLogic(IBouquetLogic bouquetLogic, IFlowerLogic flowerLogic,
       IOrderLogic orderLLogic, IStorageLogic storageLogic)
        {
            this.bouquetLogic = bouquetLogic;
            this.flowerLogic = flowerLogic;
            this.orderLogic = orderLLogic;
            this.storageLogic = storageLogic;
        }

        public List<ReportBouquetFlowerViewModel> GetBouquetFlower()
        {
            var Bouquets = bouquetLogic.Read(null);
            var list = new List<ReportBouquetFlowerViewModel>();
            foreach (var bouquet in Bouquets)
            {
                foreach (var mb in bouquet.BouquetFlowers)
                {
                    var record = new ReportBouquetFlowerViewModel
                    {
                        BouquetName = bouquet.BouquetName,
                        FlowerName = mb.Value.Item1,
                        Count = mb.Value.Item2,
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<ReportStorageFlowerViewModel> GetStorageFlowers()
        {
            var storages = storageLogic.GetList();
            var list = new List<ReportStorageFlowerViewModel>();

            foreach (var storage in storages)
            {
                foreach (var sf in storage.StorageFlowers)
                {
                    var record = new ReportStorageFlowerViewModel
                    {
                        StorageName = storage.StorageName,
                        FlowerName = sf.FlowerName,
                        Count = sf.Count
                    };

                    list.Add(record);
                }
            }
            return list;
        }
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }
        public void SaveBouquetsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список букетов",
                Bouquets = bouquetLogic.Read(null)
            });
        }
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
        public void SaveBouquetsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список цветов по букетам",
                BouquetFlowers = GetBouquetFlower(),
            });
        }
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Bouquets = null,
                Storages = storageLogic.GetList()
            });
        }

        public void SaveStorageFlowersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список цветов на складах",
                Orders = null,
                Storages = storageLogic.GetList()
            });
        }

        public void SaveFlowersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список цветов",
                BouquetFlowers = null,
                StorageFlowers = GetStorageFlowers()
            });
        }
    }
}