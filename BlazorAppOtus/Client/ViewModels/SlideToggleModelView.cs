using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAppOtus.Client.ViewModels
{
    public class SlideToggleModelView
    {
        public string Name { get; set; }
        public bool isChecked {get;set;}
        public bool isEnabled { get; set; }
        public string Attribute { get; set; }
    }
}
