using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;

namespace FlowerShopListImplement.Implements
{
    public class FlowerLogic : IFlowerLogic
    {
        private readonly DataListSingleton source;

        public FlowerLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(FlowerBindingModel model)
        {
            Flower tempFlower = model.Id.HasValue ? null : new Flower
            {
                Id = 1
            };
            foreach (var Flower in source.Flowers)
            {
                if (Flower.FlowerName == model.FlowerName && Flower.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть цветок с таким названием");
                }
                if (!model.Id.HasValue && Flower.Id >= tempFlower.Id)
                {
                    tempFlower.Id = Flower.Id + 1;
                }
                else if (model.Id.HasValue && Flower.Id == model.Id)
                {
                    tempFlower = Flower;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempFlower == null)
                {
                    throw new Exception("Цветок не найден");
                }
                CreateModel(model, tempFlower);
            }
            else
            {
                source.Flowers.Add(CreateModel(model, tempFlower));
            }
        }

        public void Delete(FlowerBindingModel model)
        {
            for (int i = 0; i < source.Flowers.Count; ++i)
            {
                if (source.Flowers[i].Id == model.Id.Value)
                {
                    source.Flowers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Цветок не найден");
        }

        public List<FlowerViewModel> Read(FlowerBindingModel model)
        {
            List<FlowerViewModel> result = new List<FlowerViewModel>();
            foreach (var Flower in source.Flowers)
            {
                if (model != null)
                {
                    if (Flower.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Flower));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Flower));
            }
            return result;
        }

        private Flower CreateModel(FlowerBindingModel model, Flower Flower)
        {
            Flower.FlowerName = model.FlowerName;
            return Flower;
        }

        private FlowerViewModel CreateViewModel(Flower Flower)
        {
            return new FlowerViewModel
            {
                Id = Flower.Id,
                FlowerName = Flower.FlowerName
            };
        }
    }
}