using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Estilos
{
    public class EstilosListView

       
    {
        public int EstiloID { get; set; }
        public int Estilo_No { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoSTD { get; set; }
        public decimal Ganancia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string PattenNo { get; set; }
        public string Last { get; set; }
        public string Comentarios { get; set; }
        public string Marcas { get; set; }
        public string Modelos { get; set; }
        public string TiposEstilos { get; set; }
        public string Categorias { get; set; }
        public string Materiales { get; set; }
        public string PesosEstilos { get; set; }
        public string Colores { get; set; }
        public string Division { get; set; }
        public string UnidadesMedidas { get; set; }
        public string Estados { get; set; }

        public List<string> Modelos1 { get; set; }
        public List<string> TiposEstilos1 { get; set; }
        public List<string> Categorias1 { get; set; }
        public List<string> Materiales1 { get; set; }
        public List<string> Pesos { get; set; }
        public List<string> Colores1 { get; set; }
    }
}
