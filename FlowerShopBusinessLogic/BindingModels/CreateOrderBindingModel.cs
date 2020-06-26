using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FlowerShopBusinessLogic.BindingModels
{
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ClientId { set; get; }

        [DataMember]
        public string ClientFIO { set; get; }

        [DataMember]
        public int BouquetId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}