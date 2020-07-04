using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using FlowerShopBusinessLogic.Attributes;

namespace FlowerShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel    
    {
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]

        [DataMember]
        public string ClientFIO { set; get; }

        [Column(title: "Почта", width: 150)]
        [DataMember]
        public string Login { set; get; }

        [DataMember]
        public string Password { set; get; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ClientFIO",
            "Login"
        };
    }
}