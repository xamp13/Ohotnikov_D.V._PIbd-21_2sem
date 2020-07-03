using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.ViewModels;
using FlowerShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowerShopListImplement.Implements
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly DataListSingleton source;

        public MessageInfoLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Create(MessageInfoBindingModel model)
        {
            for (int i = 0; i < source.MessageInfoes.Count; ++i)
            {
                if (source.MessageInfoes[i].MessageId == model.MessageId)
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
            }

            source.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
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