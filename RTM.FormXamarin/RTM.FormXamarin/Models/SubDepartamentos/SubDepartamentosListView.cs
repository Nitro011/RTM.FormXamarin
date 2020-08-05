using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.SubDepartamentos
{
    public class SubDepartamentosListView
    {
        public int SubDepartamentoID { get; set; }
        public int? DepartamentoID { get; set; }
        public string Departamento { get; set; }
        public string SubDepartamento { get; set; }
    }
}
