using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Suplidores
{
    public class Suplidore
    {
        public int SuplidorID { get; set; }
        public string Empresa { get; set; }
        public string Nombre_Suplidor { get; set; }
        public string No_Telefono { get; set; }
        public string Correo_Electronico { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
    }
}
