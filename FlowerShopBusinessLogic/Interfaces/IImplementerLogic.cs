using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.ViewModels;

namespace FlowerShopBusinessLogic.Interfaces
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);

        void CreateOrUpdate(ImplementerBindingModel model);

        void Delete(ImplementerBindingModel model);
    }
}