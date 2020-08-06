using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models.Estilos
{
    public class Estilo
    {
        public int EstiloID { get; set; }
        public int? MarcaID { get; set; }
        public int Estilo_No { get; set; }
        public int? DivisionID { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoSTD { get; set; }
        public decimal Ganancia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string PattenNo { get; set; }
        public int? EstadoID { get; set; }
        public string Last { get; set; }
        public int? UnidadMedidaEstiloID { get; set; }
        public string Comentarios { get; set; }
    }
}
