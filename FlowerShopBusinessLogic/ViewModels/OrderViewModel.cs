using FlowerShopBusinessLogic.Attributes;
using FlowerShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization;

namespace FlowerShopBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int BouquetId { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }
        [Column(title: "Клиент", width: 160)]
        [DisplayName("Клиент")]
        [DataMember]
        public string ClientFIO { get; set; }
        [Column(title: "Композиция", width: 100)]
        [DisplayName("Композиция")]
        [DataMember]
        public string BouquetName { get; set; }
        [Column(title: "Исполнитель", width: 100)]
        [DisplayName("Исполнитель")]
        [DataMember]
        public string ImplementerFIO { get; set; }
        [Column(title: "Количество", width: 72)]
        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 50)]
        [DisplayName("Сумма")]
        [DataMember]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 70)]
        [DisplayName("Статус")]
        [DataMember]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 110)]
        [DisplayName("Дата создания")]
        [DataMember]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 110)]
        [DisplayName("Дата выполнения")]
        [DataMember]
        public DateTime? DateImplement { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ClientFIO",
            "BouquetName",
            "ImplementerFIO",
            "Count",
            "Sum",
            "Status",
            "DateCreate",
            "DateImplement"
        };
    }
}