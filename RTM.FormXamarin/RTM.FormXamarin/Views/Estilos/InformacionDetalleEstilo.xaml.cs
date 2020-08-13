using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Models.Estilos;
using RTM.FormXamarin.Models.Empleados;
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionDetalleEstilo : ContentPage
    {
        public int estiloID;
        public InformacionDetalleEstilo(int EstiloID)
        {
            InitializeComponent();
            MostrarInformacionEstilos(EstiloID); 
            BindingContext = new EstilosListViewViewModel(EstiloID);
        }

        private void MostrarInformacionEstilos(int EstiloID)
        {

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/EstilosNuevos/ObtenerEstilosPorEstiloID/{EstiloID}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<EstilosListView>>(response.data.ToString()).ElementAtOrDefault(0);

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    estiloID = listaView.EstiloID;
                    Estilo_No.Text = listaView.Estilo_No.ToString();
                    Descripcion.Text = listaView.Descripcion.ToString();
                    CostoSTD.Text = listaView.CostoSTD.ToString();
                    Ganancia.Text = listaView.Ganancia.ToString();
                    FechaCreacion.Text = listaView.FechaCreacion.ToString();
                    PattenNo.Text = listaView.PattenNo.ToString();
                    Last.Text = listaView.Last.ToString();
                    Comentarios.Text = listaView.Comentarios.ToString();
                    Marcas.Text = listaView.Marcas.ToString();
                    Modelos1.Text = string.Join(",", listaView.Modelos1.ToArray());
                    //listaEstilos.ItemsSource=string.Join(",",listaView.Modelos1.ToArray(), ",", listaView.Colores1.ToArray());
                }

            }

        }
    }
}