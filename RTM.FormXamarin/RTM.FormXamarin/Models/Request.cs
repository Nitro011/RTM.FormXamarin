using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models
{
    public class Request
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
