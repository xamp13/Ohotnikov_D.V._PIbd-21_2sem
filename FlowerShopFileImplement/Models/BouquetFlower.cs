using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopFileImplement.Models
{
    public class BouquetFlower
    {
        public int Id { get; set; }

        public int BouquetId { get; set; }

        public int FlowerId { get; set; }

        public int Count { get; set; }
    }
}
