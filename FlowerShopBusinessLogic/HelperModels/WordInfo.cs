using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<BouquetViewModel> Bouquets { get; set; }

        public List<StorageViewModel> Storages { get; set; }

    }
}
