using FlowerShopBusinessLogic.BindingModels;
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
                    throw new Exception("Заказ не найден");
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
            throw new Exception("Заказ не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var Order in source.Orders)
            {
                if (model != null)
                {
                    if ((model.Id.HasValue && Order.Id == model.Id)
                        || (model.DateFrom.HasValue && model.DateTo.HasValue && Order.DateCreate >= model.DateFrom && Order.DateCreate <= model.DateTo)
                        || (Order.ClientId == model.ClientId)
                        || (model.FreeOrder.HasValue && model.FreeOrder.Value && !Order.ImplementerId.HasValue)
                        || (model.ImplementerId.HasValue && Order.ImplementerId == model.ImplementerId && Order.Status == OrderStatus.Выполняется))
                    {     
                        result.Add(CreateViewModel(Order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Order));
            }
            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order Order)
        {
            Bouquet Bouquet = null;
            foreach (Bouquet b in source.Bouquets)
            {
                if (b.Id == model.BouquetId)
                {
                    Bouquet = b;
                    break;
                }
            }
            Client client = null;
            foreach (Client c in source.Clients)
            {
                if (c.Id == model.ClientId)
                {
                    client = c;
                    break;
                }
            }
            Implementer implementer = null;
            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == model.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }
            if (Bouquet == null || client == null || model.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }
            Order.BouquetId = model.BouquetId;
            Order.ClientId = model.ClientId.Value;
            Order.ImplementerId = (int)model.ImplementerId;
            Order.Sum = model.Count * Bouquet.Price;
            Order.Count = model.Count;
            Order.Status = model.Status;
            Order.DateCreate = model.DateCreate;
            Order.DateImplement = model.DateImplement;
            return Order;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            Bouquet Bouquet = null;
            foreach (Bouquet b in source.Bouquets)
            {
                if (b.Id == order.BouquetId)
                {
                    Bouquet = b;
                    break;
                }
            }
            Client client = null;
            foreach (Client c in source.Clients)
            {
                if (c.Id == order.ClientId)
                {
                    client = c;
                    break;
                }
            }
            Implementer implementer = null;
            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == order.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }
            if (Bouquet == null || client == null || order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }
            return new OrderViewModel
            {
                Id = order.Id,
                BouquetId = order.BouquetId,
                BouquetName = Bouquet.BouquetName,
                ClientId = order.ClientId,
                ClientFIO = client.ClientFIO,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = implementer.ImplementerFIO,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}