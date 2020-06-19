using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.MateriasPrimas
{
    public class MateriaPrima
    {
        public int Materia_PrimaID { get; set; }
        public int? Tipo_MaterialID { get; set; }
        public string Nombre_Materia_Prima { get; set; }
    }
}
