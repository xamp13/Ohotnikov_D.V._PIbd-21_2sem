using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopDatabaseImplement.Models;
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order;
                        if (model.Id.HasValue)
                        {
                            order = context.Orders.ToList().FirstOrDefault(rec => rec.Id == model.Id);
                            if (order == null)
                                throw new Exception("Элемент не найден");
                            order.BouquetId = model.BouquetId;
                            order.Count = model.Count;
                            order.DateCreate = model.DateCreate;
                            order.DateImplement = model.DateImplement;
                            order.Status = model.Status;
                            order.Sum = model.Sum;
                        }
                        else
                        {
                            order = new Order();
                            order.BouquetId = model.BouquetId;
                            order.Count = model.Count;
                            order.DateCreate = model.DateCreate;
                            order.DateImplement = model.DateImplement;
                            order.Status = model.Status;
                            order.Sum = model.Sum;
                            context.Orders.Add(order);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                        if (order != null)
                        {
                            context.Orders.Remove(order);
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Orders.Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new OrderViewModel()
                {
                    Id = rec.Id,
                    BouquetId = rec.BouquetId,
                    BouquetName = context.Bouquets.FirstOrDefault((r) => r.Id == rec.BouquetId).BouquetName,
                    Count = rec.Count,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    Status = rec.Status,
                    Sum = rec.Sum
                }).ToList();
            }
        }
    }
}