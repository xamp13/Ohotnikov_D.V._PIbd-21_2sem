using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopFileImplement.Implements
{
    public class BouquetLogic : IBouquetLogic
    {
        private readonly FileDataListSingleton source;
        public BouquetLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(BouquetBindingModel model)
        {
            Bouquet element = source.Bouquets.FirstOrDefault(rec => rec.BouquetName == model.BouquetName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть букет с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Bouquets.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Bouquets.Count > 0 ? source.Flowers.Max(rec => rec.Id) : 0;
                element = new Bouquet { Id = maxId + 1 };
                source.Bouquets.Add(element);
            }
            element.BouquetName = model.BouquetName;
            element.Price = model.Price;
            source.BouquetFlowers.RemoveAll(rec => rec.BouquetId == model.Id && !model.BouquetFlowers.ContainsKey(rec.FlowerId));
            var updateFlowers = source.BouquetFlowers.Where(rec => rec.BouquetId == model.Id && model.BouquetFlowers.ContainsKey(rec.FlowerId));
            foreach (var updateFlower in updateFlowers)
            {
                updateFlower.Count = model.BouquetFlowers[updateFlower.FlowerId].Item2;
                model.BouquetFlowers.Remove(updateFlower.FlowerId);
            }
            int maxPCId = source.BouquetFlowers.Count > 0 ? source.BouquetFlowers.Max(rec => rec.Id) : 0;
            foreach (var pc in model.BouquetFlowers)
            {
                source.BouquetFlowers.Add(new BouquetFlower
                {
                    Id = ++maxPCId,
                    BouquetId = element.Id,
                    FlowerId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(BouquetBindingModel model)
        {
            source.BouquetFlowers.RemoveAll(rec => rec.BouquetId == model.Id);
            Bouquet element = source.Bouquets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Bouquets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<BouquetViewModel> Read(BouquetBindingModel model)
        {
            return source.Bouquets
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new BouquetViewModel
            {
                Id = rec.Id,
                BouquetName = rec.BouquetName,
                Price = rec.Price,
                BouquetFlowers = source.BouquetFlowers
            .Where(recPC => recPC.BouquetId == rec.Id)
           .ToDictionary(recPC => recPC.FlowerId, recPC =>
            (source.Flowers.FirstOrDefault(recC => recC.Id == recPC.FlowerId)?.FlowerName, recPC.Count))
            })
            .ToList();
        }
    }
}