﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace FlowerShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        [DisplayName("Клиент")]
        public string ClientFIO { set; get; }

        [DataMember]
        public string Login { set; get; }

        [DataMember]
        public string Password { set; get; }
    }
}