using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Usuarios
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public int? RolID { get; set; }
        public int? EmpleadoID { get; set; }
        public int? AreaProduccionID { get; set; }
        public string NombreDeUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
    }
}
