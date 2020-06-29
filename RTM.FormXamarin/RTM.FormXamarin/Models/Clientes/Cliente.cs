using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Clientes
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Correo_Electronico { get; set; }
        public string No_Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
