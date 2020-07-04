using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopListImplement.Implements
{
    public class BouquetLogic : IBouquetLogic
    {
        private readonly DataListSingleton source;

        public BouquetLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(BouquetBindingModel model)
        {
            Bouquet tempBouquet = model.Id.HasValue ? null : new Bouquet { Id = 1 };
            foreach (var Bouquet in source.Bouquets)
            {
                if (Bouquet.BouquetName == model.BouquetName && Bouquet.Id != model.Id)
                {
                    throw new Exception("Уже есть букет с таким названием");
                }
                if (!model.Id.HasValue && Bouquet.Id >= tempBouquet.Id)
                {
                    tempBouquet.Id = Bouquet.Id + 1;
                }
                else if (model.Id.HasValue && Bouquet.Id == model.Id)
                {
                    tempBouquet = Bouquet;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempBouquet == null)
                {
                    throw new Exception("Букет не найден");
                }
                CreateModel(model, tempBouquet);
            }

            else
            {
                source.Bouquets.Add(CreateModel(model, tempBouquet));
            }
        }

        public void Delete(BouquetBindingModel model)
        {
            for (int i = 0; i < source.BouquetFlowers.Count; ++i)
            {
                if (source.BouquetFlowers[i].BouquetId == model.Id)
                {
                    source.BouquetFlowers.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Bouquets.Count; ++i)
            {
                if (source.Bouquets[i].Id == model.Id)
                {
                    source.Bouquets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Букет не найден");
        }

        private Bouquet CreateModel(BouquetBindingModel model, Bouquet Bouquet)
        {
            Bouquet.BouquetName = model.BouquetName;
            Bouquet.Price = model.Price;

            int maxPCId = 0;
            for (int i = 0; i < source.BouquetFlowers.Count; ++i)
            {
                if (source.BouquetFlowers[i].Id > maxPCId)
                {
                    maxPCId = source.BouquetFlowers[i].Id;
                }
                if (source.BouquetFlowers[i].BouquetId == Bouquet.Id)
                {
                    if
                    (model.BouquetFlowers.ContainsKey(source.BouquetFlowers[i].FlowerId))
                    {
                        source.BouquetFlowers[i].Count =
                        model.BouquetFlowers[source.BouquetFlowers[i].FlowerId].Item2;


                        model.BouquetFlowers.Remove(source.BouquetFlowers[i].FlowerId);
                    }
                    else
                    {
                        source.BouquetFlowers.RemoveAt(i--);
                    }
                }
            }
            foreach (var pc in model.BouquetFlowers)
            {
                source.BouquetFlowers.Add(new BouquetFlower
                {
                    Id = ++maxPCId,
                    BouquetId = Bouquet.Id,
                    FlowerId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return Bouquet;
        }

        public List<BouquetViewModel> Read(BouquetBindingModel model)
        {
            List<BouquetViewModel> result = new List<BouquetViewModel>();
            foreach (var Flower in source.Bouquets)
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

        private BouquetViewModel CreateViewModel(Bouquet Bouquet)
        {
            Dictionary<int, (string, int)> BouquetFlowers = new Dictionary<int, (string, int)>();
            foreach (var pc in source.BouquetFlowers)
            {
                if (pc.BouquetId == Bouquet.Id)
                {
                    string FlowerName = string.Empty;
                    foreach (var Flower in source.Flowers)
                    {
                        if (pc.FlowerId == Flower.Id)
                        {
                            FlowerName = Flower.FlowerName;
                            break;
                        }
                    }
                    BouquetFlowers.Add(pc.FlowerId, (FlowerName, pc.Count));
                }
            }
            return new BouquetViewModel
            {
                Id = Bouquet.Id,
                BouquetName = Bouquet.BouquetName,
                Price = Bouquet.Price,
                BouquetFlowers = BouquetFlowers
            };
        }
    }
}