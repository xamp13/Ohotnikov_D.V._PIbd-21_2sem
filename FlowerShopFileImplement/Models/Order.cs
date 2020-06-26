﻿using FlowerShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BouquetId { get; set; }

        public int ClientId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
