using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;
        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageFlowerViewModel> StorageFlowers = new List<StorageFlowerViewModel>();
                for (int j = 0; j < source.StorageFlowers.Count; ++j)
                {
                    if (source.StorageFlowers[j].StorageId == source.Storages[i].Id)
                    {
                        string FlowerName = string.Empty;
                        for (int k = 0; k < source.Flowers.Count; ++k)
                        {
                            if (source.StorageFlowers[j].FlowerId ==
                           source.Flowers[k].Id)
                            {
                                FlowerName = source.Flowers[k].FlowerName;
                                break;
                            }
                        }
                        StorageFlowers.Add(new StorageFlowerViewModel
                        {
                            Id = source.StorageFlowers[j].Id,
                            StorageId = source.StorageFlowers[j].StorageId,
                            FlowerId = source.StorageFlowers[j].FlowerId,
                            FlowerName = FlowerName,
                            Count = source.StorageFlowers[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageFlowers = StorageFlowers
                });
            }
            return result;
        }
        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageFlowerViewModel> StorageFlowers = new List<StorageFlowerViewModel>();
                for (int j = 0; j < source.StorageFlowers.Count; ++j)
                {
                    if (source.StorageFlowers[j].StorageId == source.Storages[i].Id)
                    {
                        string FlowerName = string.Empty;
                        for (int k = 0; k < source.Flowers.Count; ++k)
                        {
                            if (source.StorageFlowers[j].FlowerId ==
                           source.Flowers[k].Id)
                            {
                                FlowerName = source.Flowers[k].FlowerName;
                                break;
                            }
                        }
                        StorageFlowers.Add(new StorageFlowerViewModel
                        {
                            Id = source.StorageFlowers[j].Id,
                            StorageId = source.StorageFlowers[j].StorageId,
                            FlowerId = source.StorageFlowers[j].FlowerId,
                            FlowerName = FlowerName,
                            Count = source.StorageFlowers[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageFlowers = StorageFlowers
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;

        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.StorageFlowers.Count; ++i)
            {
                if (source.StorageFlowers[i].StorageId == id)
                {
                    source.StorageFlowers.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void FillStorage(StorageFlowerBindingModel model)
        {
            int foundItemIndex = -1;
            for (int i = 0; i < source.StorageFlowers.Count; ++i)
            {
                if (source.StorageFlowers[i].FlowerId == model.FlowerId
                    && source.StorageFlowers[i].StorageId == model.StorageId)
                {
                    foundItemIndex = i;
                    break;
                }
            }
            if (foundItemIndex != -1)
            {
                source.StorageFlowers[foundItemIndex].Count =
                    source.StorageFlowers[foundItemIndex].Count + model.Count;
            }
            else
            {
                int maxId = 0;
                for (int i = 0; i < source.StorageFlowers.Count; ++i)
                {
                    if (source.StorageFlowers[i].Id > maxId)
                    {
                        maxId = source.StorageFlowers[i].Id;
                    }
                }
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
            throw new NotImplementedException();
        }

        public void RemoveFromStorage(int bouquetId, int bouquetsCount)
        {
            throw new NotImplementedException();
        }
    }
}