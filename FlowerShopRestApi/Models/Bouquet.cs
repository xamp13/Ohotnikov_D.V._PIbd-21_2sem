using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerShopRestApi.Models
{
    public class Bouquet
    {
        public int Id { set; get; }
        public string BouquetName { set; get; }
        public decimal Price { set; get; }
    }
}