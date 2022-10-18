using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class GenericVM
    {
        public GenericVM(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
