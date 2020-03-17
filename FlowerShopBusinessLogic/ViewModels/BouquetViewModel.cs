using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlowerShopBusinessLogic.ViewModels
{
    public class BouquetViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название композиции")]

        public string BouquetName { get; set; }

        [DisplayName("Цена")]

        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> BouquetFlowers { get; set; }
    }
}