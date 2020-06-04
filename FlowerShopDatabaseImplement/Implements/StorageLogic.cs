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
    public class StorageLogic : IStorageLogic
    {
        public List<StorageViewModel> GetList()
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Storages
                .ToList()
               .Select(rec => new StorageViewModel
               {
                   Id = rec.Id,
                   StorageName = rec.StorageName,
                   StorageFlowers = context.StorageFlowers
                .Include(recSF => recSF.Flower)
               .Where(recSF => recSF.StorageId == rec.Id).
               Select(x => new StorageFlowerViewModel
               {
                   Id = x.Id,
                   StorageId = x.StorageId,
                   FlowerId = x.FlowerId,
                   FlowerName = context.Flowers.FirstOrDefault(y => y.Id == x.FlowerId).FlowerName,
                   Count = x.Count
               })
               .ToList()
               })
            .ToList();
            }
        }

        public StorageViewModel GetElement(int id)
        {
            using (var context = new FlowerShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    return new StorageViewModel
                    {
                        Id = id,
                        StorageName = elem.StorageName,
                        StorageFlowers = context.StorageFlowers
                .Include(recSF => recSF.Flower)
               .Where(recSF => recSF.StorageId == elem.Id)
                        .Select(x => new StorageFlowerViewModel
                        {
                            Id = x.Id,
                            StorageId = x.StorageId,
                            FlowerId = x.FlowerId,
                            FlowerName = context.Flowers.FirstOrDefault(y => y.Id == x.FlowerId).FlowerName,
                            Count = x.Count
                        }).ToList()
                    };
                }
            }
        }

        public void AddElement(StorageBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var storage = new Storage();
                context.Storages.Add(storage);
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }

        public void UpdElement(StorageBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Storages.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.StorageName = model.StorageName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void DelElement(int id)
        {
            using (var context = new FlowerShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Storages.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void FillStorage(StorageFlowerBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                var item = context.StorageFlowers.FirstOrDefault(x => x.FlowerId == model.FlowerId && x.StorageId == model.StorageId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new StorageFlower();
                    context.StorageFlowers.Add(elem);
                    elem.StorageId = model.StorageId;
                    elem.FlowerId = model.FlowerId;
                    elem.Count = model.Count;
                }
                context.SaveChanges();
            }
        }

        public void RemoveFromStorage(int bouquetId, int bouquetsCount)
        {
            using (var context = new FlowerShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var bouquetFlowers = context.BouquetFlowers.Where(x => x.BouquetId == bouquetId);
                        if (bouquetFlowers.Count() == 0) return;
                        foreach (var elem in bouquetFlowers)
                        {
                            int left = elem.Count * bouquetsCount;
                            var storageFlowers = context.StorageFlowers.Where(x => x.FlowerId == elem.FlowerId);
                            int available = storageFlowers.Sum(x => x.Count);
                            if (available < left) throw new Exception("Недостаточно цветов на складе");
                            foreach (var rec in storageFlowers)
                            {
                                int toRemove = left > rec.Count ? rec.Count : left;
                                rec.Count -= toRemove;
                                left -= toRemove;
                                if (left == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}