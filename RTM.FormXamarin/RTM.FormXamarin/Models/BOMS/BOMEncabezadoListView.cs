using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.BOMS
{
    public class BOMEncabezadoListView
    {
        public int BOMID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string PatterN { get; set; }
        public string Modelo { get; set; }
        public string Cliente { get; set; }
    }
}
