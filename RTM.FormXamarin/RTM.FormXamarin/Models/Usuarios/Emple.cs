using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Usuarios
{
    public class Emple
    {

        public int RolID { get; set; }
        public bool LockoutEnabled { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? EmpleadoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool Sexo { get; set; }
        public string Cedula { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
