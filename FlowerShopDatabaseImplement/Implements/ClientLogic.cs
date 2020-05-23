﻿using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopDatabaseImplement.Models;
using System.Linq;

namespace FlowerShopDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Client client;
                if (model.Id.HasValue)
                {
                    client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (client == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    client = new Client();
                    context.Clients.Add(client);
                }
                client.ClientFIO = model.ClientFIO;
                client.Login = model.Login;
                client.Password = model.Password;
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (client != null)
                {
                    context.Clients.Remove(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new FlowerShopDatabase())
            {
                return context.Clients
                .Where(rec => model == null || rec.Id == model.Id ||
                rec.Login == model.Login && rec.Password == model.Password)
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
}