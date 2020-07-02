using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlowerShopDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { set; get; }

        public string ImplementerFIO { set; get; }

        public int WorkingTime { set; get; }

        public int PauseTime { set; get; }

        public virtual List<Order> Orders { set; get; }
    }
}