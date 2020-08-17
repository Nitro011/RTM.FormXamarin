using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.OrdenesClientes
{
    public class OrdenesClientesListView
    {
        public int Orden_ClienteID { get; set; }
        public string CodigoQR { get; set; }
        public string Cliente { get; set; }
        public int CantidadCalzados { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaEntrega { get; set; }
        public List<int> Estilos { get; set; }
        public string SizesUSA { get; set; }
        public string SizesUK { get; set; }
        public string SizesEuro { get; set; }
        public string SizesCM { get; set; }
    }
}
