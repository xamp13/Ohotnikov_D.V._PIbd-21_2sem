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

        public List<IGrouping<DateTime, OrderViewModel>> Orders { get; set; }
        public List<StorageViewModel> Storages { get; set; }
    }
}