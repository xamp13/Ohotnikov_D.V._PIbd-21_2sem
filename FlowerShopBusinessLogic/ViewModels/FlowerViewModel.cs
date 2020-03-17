using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlowerShopBusinessLogic.ViewModels
{
    public class FlowerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название цветка")]

        public string FlowerName { get; set; }
    }
}