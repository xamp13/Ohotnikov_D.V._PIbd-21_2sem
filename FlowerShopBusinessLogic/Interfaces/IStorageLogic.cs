using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> GetList();

        StorageViewModel GetElement(int id);

        void AddElement(StorageBindingModel model);

        void UpdElement(StorageBindingModel model);

        void DelElement(int id);

        void FillStorage(StorageFlowerBindingModel model);

        bool CheckFlowersAvailability(int bouquetId, int bouquetsCount);

        void RemoveFromStorage(int bouquetId, int bouquetsCount);
    }
}