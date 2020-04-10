﻿using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.ViewModels;
namespace FlowerShopBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}