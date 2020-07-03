using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.Enums;
using FlowerShopBusinessLogic.Interfaces;
using FlowerShopBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopBusinessLogic.BusinessLogics
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic storageLogic;
        private readonly IClientLogic clientLogic;
        private readonly object locker = new object();
        public MainLogic(IOrderLogic orderLogic, IStorageLogic storageLogic, IClientLogic clientLogic)
        {
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
            this.clientLogic = clientLogic;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                BouquetId = model.BouquetId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = model.ClientId })?[0]?.Login,
                Subject = $"Новый заказ",
                Text = $"Заказ принят."
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = orderLogic.Read(new OrderBindingModel
                {
                    Id = model.OrderId
                })?[0];
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_цветы)
                {
                    throw new Exception("Заказ не в статусе \"Принят\"или \"Требуются цветы\"");
                }
                if (order.ImplementerId.HasValue)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }
                var orderModel = new OrderBindingModel
                {
                    Id = order.Id,
                    BouquetId = order.BouquetId,
                    ClientId = order.ClientId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                };
                try
                {
                    storageLogic.RemoveFromStorage(order.BouquetId, order.Count);
                    orderModel.DateImplement = DateTime.Now;
                    orderModel.Status = OrderStatus.Выполняется;
                    orderModel.ImplementerId = model.ImplementerId;

                    MailLogic.MailSendAsync(new MailSendInfo
                    {
                        MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                        Subject = $"Заказ №{order.Id}",
                        Text = $"Заказ №{order.Id} передан в работу."
                    });
                }
                catch
                {
                    orderModel.Status = OrderStatus.Требуются_цветы;
                    MailLogic.MailSendAsync(new MailSendInfo
                    {
                        MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                        Subject = $"Заказ №{order.Id}",
                        Text = $"Заказ №{order.Id} требует цветы."
                    });
                }
                orderLogic.CreateOrUpdate(orderModel);
            }
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                BouquetId = order.BouquetId,
                Count = order.Count,
                Sum = order.Sum,
                ImplementerId = order.ImplementerId,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} готов."
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                BouquetId = order.BouquetId,
                Count = order.Count,
                Sum = order.Sum,
                ImplementerId = order.ImplementerId,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel { Id = order.ClientId })?[0]?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }
        public void FillStorage(StorageFlowerBindingModel model)
        {
            storageLogic.FillStorage(model);
        }
    }
}