using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlowerShopBusinessLogic.ViewModels
{
    public class StorageFlowerViewModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int FlowerId { get; set; }

        [DisplayName("Название цветка")]
        public string FlowerName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}