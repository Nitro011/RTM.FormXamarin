using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Usuarios
{
    public class UsuariosByID
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string puesto { get; set; }
        public bool sexo { get; set; }
        public string cedula { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
    }
}
