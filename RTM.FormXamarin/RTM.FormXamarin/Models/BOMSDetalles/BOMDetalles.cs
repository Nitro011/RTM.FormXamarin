using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.BOMSDetalles
{
    public class BOMDetalles
    {
        public int BOMDetalleID { get; set; }
        public int BOMID { get; set; }
        public string PartNo { get; set; }
        public string DIE { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal Usage { get; set; }
        public decimal Cost { get; set; }
        public decimal Ext { get; set; }
    }
}
