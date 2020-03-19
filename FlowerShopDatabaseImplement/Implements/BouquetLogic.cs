using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopDatabaseImplement.Implements
{
    public class BouquetLogic : IBouquetLogic
    {
        public void CreateOrUpdate(BouquetBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Bouquet element = context.Bouquets.FirstOrDefault(rec =>
                       rec.BouquetName == model.BouquetName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть букет с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Bouquets.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Bouquet();
                            context.Bouquets.Add(element);
                        }
                        element.BouquetName = model.BouquetName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var BouquetFlowers = context.BouquetFlowers.Where(rec
                           => rec.BouquetId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.BouquetFlowers.RemoveRange(BouquetFlowers.Where(rec =>
                            !model.BouquetFlowers.ContainsKey(rec.FlowerId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateFlower in BouquetFlowers)
                            {
                                updateFlower.Count =
                               model.BouquetFlowers[updateFlower.FlowerId].Item2;

                                model.BouquetFlowers.Remove(updateFlower.FlowerId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.BouquetFlowers)
                        {
                            context.BouquetFlowers.Add(new BouquetFlower
                            {
                                BouquetId = element.Id,
                                FlowerId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
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
        public void Delete(BouquetBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.BouquetFlowers.RemoveRange(context.BouquetFlowers.Where(rec =>
                        rec.BouquetId == model.Id));
                        Bouquet element = context.Bouquets.FirstOrDefault(rec => rec.Id
                       == model.Id);
                        if (element != null)
                        {
                            context.Bouquets.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
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
        public List<BouquetViewModel> Read(BouquetBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Bouquets
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new BouquetViewModel
               {
                   Id = rec.Id,
                   BouquetName = rec.BouquetName,
                   Price = rec.Price,
                   BouquetFlowers = context.BouquetFlowers
                .Include(recPC => recPC.Flower)
               .Where(recPC => recPC.BouquetId == rec.Id)
               .ToDictionary(recPC => recPC.FlowerId, recPC =>
                (recPC.Flower?.FlowerName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}