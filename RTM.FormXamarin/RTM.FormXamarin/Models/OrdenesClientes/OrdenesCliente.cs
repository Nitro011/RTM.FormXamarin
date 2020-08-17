using System;
using System.Collections.Generic;
using System.Text;
using RTM.FormXamarin.Models.OrdenesClientesEstilos;
using RTM.FormXamarin.Models.OrdenesClientesSizes;

namespace RTM.FormXamarin.Models.OrdenesClientes
{
    public class OrdenesCliente
    {
        public int Orden_ClienteID { get; set; }
        public int? ClienteID { get; set; }
        public string CodigoQR { get; set; }
        public int? Cantidad_Calzado_Realizar { get; set; }
        public System.DateTime Fecha_Inicio { get; set; }
        public System.DateTime Fecha_Entrega { get; set; }
        public List<OrdenesClientes_Estilos> OrdenesClientes_Estilos { get; set; }
        public List<OrdenesClientes_Sizes> OrdenesClientes_Sizes { get; set; }
    }
}
