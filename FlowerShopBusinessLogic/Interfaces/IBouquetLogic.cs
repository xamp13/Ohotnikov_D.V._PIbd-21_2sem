using FlowerShopBusinessLogic.ViewModels;
using FlowerShopBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.Interfaces
{
    public interface IBouquetLogic
    {
        List<BouquetViewModel> Read(BouquetBindingModel model);
        void CreateOrUpdate(BouquetBindingModel model);
        void Delete(BouquetBindingModel model);
    }
}