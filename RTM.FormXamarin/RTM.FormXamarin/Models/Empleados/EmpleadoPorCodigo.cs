using Java.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Empleados
{
    public class EmpleadoPorCodigo
    {
        public int empleadoID { get; set; }
        public string nombresApellidos { get; set; }
        public string sexo { get; set; }
        public string cedula { get; set; }
        public string fecha_Nacimiento { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string puesto { get; set; }
        public string nombreUsuario { get; set; }
        public string rol { get; set; }
    }
}
