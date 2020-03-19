using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlowerShopDatabaseImplement.Models
{
    public class Bouquet
    {
        public int Id { get; set; }

        [Required]
        public string BouquetName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("BouquetId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("BouquetId")]
        public virtual List<BouquetFlower> BouquetFlowers { get; set; }
    }
}