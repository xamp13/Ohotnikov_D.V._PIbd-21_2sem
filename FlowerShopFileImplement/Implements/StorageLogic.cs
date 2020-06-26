using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopFileImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly FileDataListSingleton source;
        public StorageLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            return source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageFlowers = source.StorageFlowers.Where(z => z.StorageId == rec.Id).Select(x => new StorageFlowerViewModel
                {
                    Id = x.Id,
                    StorageId = x.StorageId,
                    FlowerId = x.FlowerId,
                    FlowerName = source.Flowers.FirstOrDefault(y => y.Id == x.FlowerId)?.FlowerName,
                    Count = x.Count
                }).ToList()
            })
                .ToList();
        }
        public StorageViewModel GetElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
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
                    StorageFlowers = source.StorageFlowers.Where(z => z.StorageId == elem.Id).Select(x => new StorageFlowerViewModel
                    {
                        Id = x.Id,
                        StorageId = x.StorageId,
                        FlowerId = x.FlowerId,
                        FlowerName = source.Flowers.FirstOrDefault(y => y.Id == x.FlowerId)?.FlowerName,
                        Count = x.Count
                    }).ToList()
                };
            }
        }

        public void AddElement(StorageBindingModel model)
        {

            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            var elemToUpdate = source.Storages.FirstOrDefault(x => x.Id == model.Id);
            if (elemToUpdate != null)
            {
                elemToUpdate.StorageName = model.StorageName;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void DelElement(StorageBindingModel model)
        {
            source.StorageFlowers.RemoveAll(rec => rec.StorageId == model.Id);
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void FillStorage(StorageFlowerBindingModel model)
        {
            var item = source.StorageFlowers.FirstOrDefault(x => x.FlowerId == model.FlowerId
                    && x.StorageId == model.StorageId);

            if (item != null)
            {
                item.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageFlowers.Count > 0 ? source.StorageFlowers.Max(rec => rec.Id) : 0;
                source.StorageFlowers.Add(new StorageFlower
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    FlowerId = model.FlowerId,
                    Count = model.Count
                });
            }
        }

        public bool CheckFlowersAvailability(int bouquetId, int bouquetsCount)
        {
            var bouquetFlowers = source.BouquetFlowers.Where(x => x.BouquetId == bouquetId);
            if (bouquetFlowers.Count() == 0) return false;
            foreach (var elem in bouquetFlowers)
            {
                int count = source.StorageFlowers.FindAll(x => x.FlowerId == elem.FlowerId).Sum(rec => rec.Count);
                if (count < elem.Count * bouquetsCount) return false;
            }
            return true;
        }

        public void RemoveFromStorage(int bouquetId, int bouquetsCount)
        {
            var bouquetFlowers = source.BouquetFlowers.Where(x => x.BouquetId == bouquetId);
            if (bouquetFlowers.Count() == 0) return;
            foreach (var elem in bouquetFlowers)
            {
                int left = elem.Count * bouquetsCount;
                var storageFlowers = source.StorageFlowers.FindAll(x => x.FlowerId == elem.FlowerId);
                foreach (var rec in storageFlowers)
                {
                    int toRemove = left > rec.Count ? rec.Count : left;
                    rec.Count -= toRemove;
                    left -= toRemove;
                    if (left == 0) break;
                }
            }
            return;
        }
    }
}