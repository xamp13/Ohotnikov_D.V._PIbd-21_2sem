using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.ViewModels;

namespace FlowerShopBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportBouquetFlowerViewModel> BouquetFlowers { get; set; }
    }
}