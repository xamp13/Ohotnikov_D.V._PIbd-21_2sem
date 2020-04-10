using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.BusinessLogics;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopRestApi.Models;


namespace FlowerShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IBouquetLogic _bouquet;
        private readonly MainLogic _main;

        public MainController(IOrderLogic order, IBouquetLogic bouquet, MainLogic main)
        {
            _order = order;
            _bouquet = bouquet;
            _main = main;
        }

        [HttpGet]
        public List<Bouquet> GetBouquetList() => _bouquet.Read(null)?.Select(rec => Convert(rec)).ToList();

        [HttpGet]
        public Bouquet GetBouquet(int bouquetId) => Convert(_bouquet.Read(new BouquetBindingModel
        {
            Id = bouquetId
        })?[0]);

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        {
            ClientId = clientId
        });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);

        private Bouquet Convert(BouquetViewModel model)
        {
            if (model == null) return null;
            return new Bouquet
            {
                Id = model.Id,
                BouquetName = model.BouquetName,
                Price = model.Price
            };
        }
    }
}