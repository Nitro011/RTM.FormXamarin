using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.OrdenesClientesDetalles
{
    public class OrdenesClientesDetallesListView
    {
        public int Orden_Cliente_DetalleID { get; set; }
        public int? Orden_ClienteID { get; set; }
        public int? MarcaID { get; set; }
        public int? Ordenes_Clientes_Detalles_ColoresID { get; set; }
        public int? Ordenes_Clientes_Detalles_DimensionesID { get; set; }
        public int? Ordenes_Clientes_Detalles_ModelosID { get; set; }
        public int? Ordenes_Clientes_Detalles_Tipos_CalzadosID { get; set; }
    }
}
