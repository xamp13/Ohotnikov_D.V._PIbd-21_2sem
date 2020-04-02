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

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportOrdersViewModel> Orders { get; set; }
    }
}