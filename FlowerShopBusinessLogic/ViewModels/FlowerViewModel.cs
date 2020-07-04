using FlowerShopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlowerShopBusinessLogic.ViewModels
{
    public class FlowerViewModel : BaseViewModel
    {
        [Column(title: "Цветок", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FlowerName { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FlowerName"
        };
    }
}