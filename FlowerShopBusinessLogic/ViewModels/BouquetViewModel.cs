using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization;
using FlowerShopBusinessLogic.Attributes;

namespace FlowerShopBusinessLogic.ViewModels
{
    [DataContract]
    public class BouquetViewModel : BaseViewModel
    {
        [DataMember]
        public string BouquetName { get; set; }

        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> BouquetFlowers { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "BouquetName",
            "Price"
        };
    }
}