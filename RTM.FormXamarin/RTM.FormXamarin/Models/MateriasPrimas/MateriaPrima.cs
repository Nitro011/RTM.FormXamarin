using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.MateriasPrimas
{
    public class MateriaPrima
    {
        public int Materia_PrimaID { get; set; }
        public int? Tipo_MaterialID { get; set; }
        public int? DivisionMateriaPrimaID { get; set; }
        public string PartNo { get; set; }
        public string Descripcion { get; set; }
        public string Unit { get; set; }
        public decimal Cost { get; set; }
    }
}
