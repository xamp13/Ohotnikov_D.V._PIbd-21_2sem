using FlowerShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BouquetId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public int ClientId { set; get; }

        public string ClientFIO { set; get; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}