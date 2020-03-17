using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int BouquetId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}