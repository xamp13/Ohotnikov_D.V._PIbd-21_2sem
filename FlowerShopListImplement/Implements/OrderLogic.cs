using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;
using FlowerShopBusinessLogic.Enums;
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
            foreach (var order in source.Orders)
            {
                if (model != null)
                {
                    if (order.Id == model.Id
                       || (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                       || (model.ClientId.HasValue && order.ClientId == model.ClientId)
                       || (model.FreeOrder.HasValue && model.FreeOrder.Value && !order.ImplementerId.HasValue)
                       || (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется)
                       || (model.NotEnoughFlowersOrders.HasValue && model.NotEnoughFlowersOrders.Value && order.Status == OrderStatus.Требуются_цветы))
                    {
                        result.Add(CreateViewModel(order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(order));
            }
            return result;
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.BouquetId = model.BouquetId == 0 ? order.BouquetId : model.BouquetId;
            order.ClientId = (int)model.ClientId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.ImplementerId = model.ImplementerId;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateViewModel(Order order)
        {
            string bouquetName = "";
            for (int j = 0; j < source.Bouquets.Count; ++j)
            {
                if (source.Bouquets[j].Id == order.BouquetId)
                {
                    bouquetName = source.Bouquets[j].BouquetName;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                BouquetName = bouquetName,
                ClientId = order.ClientId,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                ImplementerId = order.ImplementerId,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}