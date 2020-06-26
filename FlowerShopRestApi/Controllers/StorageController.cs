using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopRestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinerRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageLogic _storage;
        private readonly IFlowerLogic _flower;
        public StorageController(IStorageLogic storage, IFlowerLogic flower)
        {
            _storage = storage;
            _flower = flower;
        }
        [HttpGet]
        public List<StorageModel> GetStoragesList() => _storage.GetList()?.Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public List<FlowerViewModel> GetFlowersList() => _flower.Read(null)?.ToList();
        [HttpGet]
        public StorageModel GetStorage(int StorageId) => Convert(_storage.GetElement(StorageId));
        [HttpPost]
        public void CreateOrUpdateStorage(StorageBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _storage.UpdElement(model);
            }
            else
            {
                _storage.AddElement(model);
            }
        }
        [HttpPost]
        public void DeleteStorage(StorageBindingModel model) => _storage.DelElement(model);
        [HttpPost]
        public void FillStorage(StorageFlowerBindingModel model) => _storage.FillStorage(model);
        private StorageModel Convert(StorageViewModel model)
        {
            if (model == null) return null;

            return new StorageModel
            {
                Id = model.Id,
                StorageName = model.StorageName,
                StorageFlowers = model.StorageFlowers
            };
        }
    }
}