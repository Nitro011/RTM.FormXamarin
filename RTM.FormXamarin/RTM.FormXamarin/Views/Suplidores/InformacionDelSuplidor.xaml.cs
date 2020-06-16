using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Suplidores;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionDelSuplidor : ContentPage
    {
        public InformacionDelSuplidor(int id)
        {
            InitializeComponent();
            MostrarInformacionSuplidor(id);
        }

        private void MostrarInformacionSuplidor(int id)
        {

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Suplidores/SuplidorPorCodigo/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<SuplidorPorCodigo>(response.data.ToString());

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    suplidorID.Text = listaView.suplidorID.ToString();
                    empresa.Text = listaView.empresa;
                    nombre_Suplidor.Text = listaView.nombre_Suplidor;
                    no_Telefono.Text = listaView.no_Telefono;
                    correo_Electronico.Text = listaView.correo_Electronico;
                    pais.Text = listaView.pais;
                    ciudad.Text = listaView.ciudad;
                    direccion.Text = listaView.direccion;
                }

            }

        }
    }
}