using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Usuarios
{
    public class UsuariosByID
    {
        public int? EmpleadoID { get; set; }
        public string NombresApellidos { get; set; }
        public string Sexo { get; set; }
        public string Cedula { get; set; }
        public System.DateTime? Fecha_Nacimiento { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string NombreUsuario { get; set; }

        public string Puesto { get; set; }
        public string Rol { get; set; }
    }
}
