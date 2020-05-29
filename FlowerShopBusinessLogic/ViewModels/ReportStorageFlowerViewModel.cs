using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.ViewModels
{
    public class ReportStorageFlowerViewModel
    {
        public string StorageName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Flowers { get; set; }
    }
}
