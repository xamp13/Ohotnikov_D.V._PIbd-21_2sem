using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopDatabaseImplement.Models;
using FlowerShopBusinessLogic.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Order order;
                if (model.Id.HasValue)
                {
                    order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (order == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    order = new Order();
                    context.Orders.Add(order);
                }
                order.BouquetId = model.BouquetId == 0 ? order.BouquetId : model.BouquetId;
                order.Count = model.Count;
                order.Sum = model.Sum;
                order.ClientFIO = model.ClientFIO;
                order.ClientId = model.ClientId == null ? order.ClientId : (int)model.ClientId;
                order.ImplementerFIO = model.ImplementerFIO;
                order.ImplementerId = model.ImplementerId;
                order.Status = model.Status;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Orders
                .Where(
                    rec => model == null
                   || rec.Id == model.Id && model.Id.HasValue
                   || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo
                   || model.ClientId.HasValue && model.ClientId == rec.ClientId
                   || model.FreeOrder.HasValue && model.FreeOrder.Value && !rec.ImplementerId.HasValue
                   || model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется
            )
            .Include(rec => rec.Bouquet)
            .Include(rec => rec.Client)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                BouquetId = rec.BouquetId,
                BouquetName = rec.Bouquet.BouquetName,
                Count = rec.Count,
                Sum = rec.Sum,
                ClientFIO = rec.Client.ClientFIO,
                ClientId = rec.ClientId,
                ImplementerId = rec.ImplementerId,
                ImplementerFIO = !string.IsNullOrEmpty(rec.ImplementerFIO) ? rec.ImplementerFIO : string.Empty,
                Status = rec.Status,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement
            })
            .ToList();
            }
        }
    }
}