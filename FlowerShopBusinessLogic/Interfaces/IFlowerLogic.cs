using FlowerShopBusinessLogic.ViewModels;
using FlowerShopBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.Interfaces
{
    public interface IFlowerLogic
    {
        List<FlowerViewModel> Read(FlowerBindingModel model);

        void CreateOrUpdate(FlowerBindingModel model);

        void Delete(FlowerBindingModel model);
    }
}