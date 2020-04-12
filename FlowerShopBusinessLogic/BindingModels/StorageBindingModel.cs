using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public List<StorageFlowerBindingModel> StorageFlowers { get; set; }
    }
}