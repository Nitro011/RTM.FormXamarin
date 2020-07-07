using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.OperacionesCalzados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.OperacionesCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionOperacionesCalzados : ContentPage
    {
        public InformacionOperacionesCalzados(int id)
        {
            InitializeComponent();
            MostrarInformacionOperacionesCalzados(id);
        }

        private void MostrarInformacionOperacionesCalzados(int id)
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

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    PartNo.Text = listaView.PartNo;
                    Descripcion.Text = listaView.Descripcion;
                    CantidadOperaciones.Text = listaView.CantidadOperaciones.ToString();
                    CostoOperacional.Text = listaView.CostoOperacional.ToString();
                }

            }

        }
    }
}