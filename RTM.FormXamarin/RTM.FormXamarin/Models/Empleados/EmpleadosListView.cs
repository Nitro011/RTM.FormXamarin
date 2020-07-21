using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Empleados
{
    public class EmpleadosListView
    {
        public int EmpleadoID { get; set; }
        public int? RolID { get; set; }
        public int? AreaProduccionID { get; set; }
        public string CodigoEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool? Sexo { get; set; }
        public string Cedula { get; set; }
        public System.DateTime? Fecha_Nacimiento { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public System.DateTime? FechaIngreso { get; set; }
    }
}
