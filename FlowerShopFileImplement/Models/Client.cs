using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopFileImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientFIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}