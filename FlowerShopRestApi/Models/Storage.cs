using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShopBusinessLogic.ViewModels;

namespace FlowerShopRestApi.Models
{
    public class StorageModel
    {
        public int Id { get; set; }
        public string StorageName { get; set; }
        public List<StorageFlowerViewModel> StorageFlowers { get; set; }
    }
}