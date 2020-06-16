using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Suplidores
{
    public class SuplidorPorCodigo
    {
        public int suplidorID { get; set; }
        public string empresa { get; set; }
        public string nombre_Suplidor { get; set; }
        public string no_Telefono { get; set; }
        public string correo_Electronico { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
    }
}
