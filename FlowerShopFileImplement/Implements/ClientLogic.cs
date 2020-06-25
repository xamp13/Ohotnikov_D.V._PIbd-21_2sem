using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopListImplement.Models;
using System.Linq;

namespace FlowerShopFileImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        private readonly FileDataListSingleton source;

        public ClientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ClientBindingModel model)
        {
            Client client = source.Clients.FirstOrDefault(rec => rec.Login == model.Login && rec.Id != model.Id);
            if (client != null)
            {
                throw new Exception("Уже есть клиент с таким логином");
            }
            if (model.Id.HasValue)
            {
                client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (client == null)
                {
                    throw new Exception("Клиент не найден");
                }
            }
            else
            {
                int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
                client = new Client { Id = maxId + 1 };
                source.Clients.Add(client);
            }
            client.Login = model.Login;
            client.ClientFIO = model.ClientFIO;
            client.Password = model.Password;
        }
        public void Delete(ClientBindingModel model)
        {
            Client client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client != null)
            {
                source.Clients.Remove(client);
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            return source.Clients
            .Where(
            rec => model == null
            || (rec.Id == model.Id)
            || (rec.Login == model.Login && rec.Password == model.Password)
            )
            .Select(rec => new ClientViewModel
            {
                Id = rec.Id,
                ClientFIO = rec.ClientFIO,
                Login = rec.Login,
                Password = rec.Password
            })
            .ToList();
        }
    }
}