using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.MateriasPrimas
{
    public class MateriasPrimasListView
    {
        public int Materia_PrimaID { get; set; }
        public string PartNo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cost { get; set; }
        public string Unit { get; set; }
        public string TipoMaterial { get; set; }
    }
}

