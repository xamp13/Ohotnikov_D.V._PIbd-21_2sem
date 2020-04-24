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
    public class FlowerLogic : IFlowerLogic
    {
        private readonly FileDataListSingleton source;
        public FlowerLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(FlowerBindingModel model)
        {
            Flower element = source.Flowers.FirstOrDefault(rec => rec.FlowerName
           == model.FlowerName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть цветок с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Flowers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Flowers.Count > 0 ? source.Flowers.Max(rec =>
               rec.Id) : 0;
                element = new Flower { Id = maxId + 1 };
                source.Flowers.Add(element);
            }
            element.FlowerName = model.FlowerName;
        }
        public void Delete(FlowerBindingModel model)
        {
            Flower element = source.Flowers.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Flowers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<FlowerViewModel> Read(FlowerBindingModel model)
        {
            return source.Flowers
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new FlowerViewModel
            {
                Id = rec.Id,
                FlowerName = rec.FlowerName
            })
            .ToList();
        }
    }
}