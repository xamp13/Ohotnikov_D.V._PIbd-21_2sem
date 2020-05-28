using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopDatabaseImplement.Implements
{
    public class FlowerLogic : IFlowerLogic
    {
        public void CreateOrUpdate(FlowerBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Flower element = context.Flowers.FirstOrDefault(rec =>
               rec.FlowerName == model.FlowerName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Flowers.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Flower();
                    context.Flowers.Add(element);
                }
                element.FlowerName = model.FlowerName;
                context.SaveChanges();
            }
        }
        public void Delete(FlowerBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Flower element = context.Flowers.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Flowers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<FlowerViewModel> Read(FlowerBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Flowers
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
}