using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.BOMS
{
    public class BOMEncabezado
    {
        public int BOMID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string PatterN { get; set; }
        public int ModeloID { get; set; }
        public int ClienteID { get; set; }
    }
}
