using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlowerShopDatabaseImplement.Models
{
    public class StorageFlower
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int FlowerId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Flower Flower { get; set; }
        public virtual Storage Storage { get; set; }
    }
}