﻿using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopBusinessLogic.Enums;
using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {

        private readonly DataListSingleton source;

        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order
            {
                Id = 1
            };
            foreach (var Order in source.Orders)
            {
                if (!model.Id.HasValue && Order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = Order.Id + 1;
                }
                else if (model.Id.HasValue && Order.Id == model.Id)
                {
                    tempOrder = Order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var Order in source.Orders)
            {
                if (
                   model != null && Order.Id == model.Id
                   || model.DateFrom.HasValue && model.DateTo.HasValue && Order.DateCreate >= model.DateFrom && Order.DateCreate <= model.DateTo
                   || model.ClientId.HasValue && Order.ClientId == model.ClientId
                   || model.FreeOrder.HasValue && model.FreeOrder.Value
                   || model.ImplementerId.HasValue && Order.ImplementerId == model.ImplementerId && Order.Status == OrderStatus.Выполняется
               )

                    result.Add(CreateViewModel(Order));
            }
            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order Order)
        {
            Order.BouquetId = model.BouquetId;
            Order.Count = model.Count;
            Order.Sum = model.Sum;
            Order.ClientId = (int)model.ClientId;
            Order.ClientFIO = model.ClientFIO;
            Order.ImplementerId = model.ImplementerId;
            Order.ImplementerFIO = model.ImplementerFIO;
            Order.Status = model.Status;
            Order.DateCreate = model.DateCreate;
            Order.DateImplement = model.DateImplement;
            return Order;
        }

        private OrderViewModel CreateViewModel(Order Order)
        {
            string BouquetName = "";
            for (int j = 0; j < source.Bouquets.Count; ++j)
            {
                if (source.Bouquets[j].Id == Order.BouquetId)
                {
                    BouquetName = source.Bouquets[j].BouquetName;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = Order.Id,
                BouquetName = BouquetName,
                Count = Order.Count,
                Sum = Order.Sum,
                BouquetId = Order.BouquetId,
                ClientId = Order.ClientId,
                ClientFIO = Order.ClientFIO,
                ImplementerId = Order.ImplementerId,
                ImplementerFIO = Order.ImplementerFIO,
                Status = Order.Status,
                DateCreate = Order.DateCreate,
                DateImplement = Order.DateImplement
            };
        }
    }
}