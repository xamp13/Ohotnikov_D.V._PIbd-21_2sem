using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowerShopBusinessLogic.ViewModels;

namespace FlowerShopBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportBouquetFlowerViewModel> BouquetFlowers { get; set; }
        public List<ReportStorageFlowerViewModel> StorageFlowers { get; set; }
    }
}