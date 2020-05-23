﻿using System;
using FlowerShopBusinessLogic.Enums;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public int BouquetId { get; set; }

        public int? ClientId { set; get; }

        public string ClientFIO { set; get; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string ImplementerFIO { set; get; }

        public int? ImplementerId { set; get; }

        public bool? FreeOrder { set; get; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}