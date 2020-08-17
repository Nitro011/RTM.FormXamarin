using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.OperacionesCalzados;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RTM.FormXamarin.Models;

namespace RTM.FormXamarin.ViewModels
{
    public class ListadoOperacionesCalzadosListView
    {
        public int OperacionesCalzadosID { get; set; }
        public string PartNo { get; set; }
        public int CantidadOperaciones { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoOperacional { get; set; }

        public ListadoOperacionesCalzadosListView(int OperacionCalzadoID)
        {
            mostrarInformacionOperacionCalzado(OperacionCalzadoID);
        }

        private void mostrarInformacionOperacionCalzado(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/OperacionesCalzados/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<OperacionesCalzadosListView>(response.data.ToString());

                    OperacionesCalzadosID = listaView.OperacionesCalzadosID;
                    PartNo = listaView.PartNo;
                    CantidadOperaciones = listaView.CantidadOperaciones;
                    Descripcion = listaView.Descripcion;
                    CostoOperacional = listaView.CostoOperacional;

                }

            }
        }
    }
}
