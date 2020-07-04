﻿using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopFileImplement.Models;
using FlowerShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order element;
            if (model.Id.HasValue)
            {
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Заказ не найден");
                }
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
                element = new Order { Id = maxId + 1 };
                source.Orders.Add(element);
            }
            element.BouquetId = model.BouquetId == 0 ? element.BouquetId : model.BouquetId;
            element.Count = model.Count;
            element.ClientId = model.ClientId == null ? element.ClientId : (int)model.ClientId;
            element.Sum = model.Sum;
            element.Status = model.Status;
            element.DateCreate = model.DateCreate;
            element.DateImplement = model.DateImplement;
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Заказ не найден");
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
            .Where(rec => (model == null || rec.Id == model.Id)
            || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
            || (model.ClientId.HasValue && rec.ClientId == model.ClientId)
            || (model.FreeOrder.HasValue && model.FreeOrder.Value && !rec.ImplementerId.HasValue)
            || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется))
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                BouquetName = source.Bouquets.FirstOrDefault(x => x.Id == rec.BouquetId)?.BouquetName,
                Count = rec.Count,
                Sum = rec.Sum,
                BouquetId = rec.BouquetId,
                ClientFIO = source.Clients.FirstOrDefault(recC => recC.Id == rec.ClientId)?.ClientFIO,
                ClientId = rec.ClientId,
                ImplementerFIO = source.Implementers.FirstOrDefault(i => i.Id == rec.ImplementerId)?.ImplementerFIO,
                ImplementerId = rec.ImplementerId,
                Status = rec.Status,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement
            })
            .ToList();
        }
    }
}
