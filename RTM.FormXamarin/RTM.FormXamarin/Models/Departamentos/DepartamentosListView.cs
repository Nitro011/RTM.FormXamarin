using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Departamentos
{
    public class DepartamentosListView
    {
        public int DepartamentoID { get; set; }
        public int? TipoDepartamentoID { get; set; }
        public string TipoDepartamento { get; set; }
        public string Departamento { get; set; }
        public string SubDepartamento { get; set; }
    }
}
