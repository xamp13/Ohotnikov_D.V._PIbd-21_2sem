using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement.Models
{
    public class StorageFlower
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int FlowerId { get; set; }

        public int Count { get; set; }
    }
}