using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlowerShopDatabaseImplement.Models
{
    public class Client
    {
        public int Id { set; get; }

        [Required]
        public string ClientFIO { set; get; }

        [Required]
        public string Login { set; get; }

        [Required]
        public string Password { set; get; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { set; get; }

        public List<MessageInfo> MessageInfoes { get; set; }
    }
}