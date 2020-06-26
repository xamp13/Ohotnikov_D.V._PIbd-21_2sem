using FlowerShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlowerShopDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { set; get; }

        public int BouquetId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public int? ImplementerId { set; get; }

        public string ImplementerFIO { set; get; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Client Client { set; get; }

        public virtual Bouquet Bouquet { get; set; }

        public virtual Implementer Implementer { set; get; }
    }
}