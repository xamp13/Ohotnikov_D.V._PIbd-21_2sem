using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopBusinessLogic.BindingModels;
using FlowerShopFileImplement.Models;
using System.Linq;

namespace FlowerShopFileImplement.Implements
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly FileDataListSingleton source;

        public MessageInfoLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void Create(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessageInfoes.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            int? clientId = source.Clients.FirstOrDefault(rec => rec.Login == model.FromMailAddress)?.Id;
            source.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = clientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });
        }

        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            List<MessageInfoViewModel> result = source.MessageInfoes
            .Where(rec => model == null || rec.ClientId == model.ClientId)
            .Skip(model.Skip)
            .Take(model.Take)
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            })
            .ToList();

            return result;
        }
    }
}