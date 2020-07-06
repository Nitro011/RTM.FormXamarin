using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Clientes
{
    public class ClientesListView
    {
        public int ClienteID { get; set; }
        public string CodigoCliente { get; set; }
        public string RNC { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Correo_Electronico { get; set; }
        public string No_Telefono { get; set; }
        public string Pais { get; set; }
        public string Direccion { get; set; }
    }
}
