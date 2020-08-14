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
using RTM.FormXamarin.Models.MateriasPrimas;
using RTM.FormXamarin.Models.TiposMateriales;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.MateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarMateriasPrimas : ContentPage
    {
        public int Materia_PrimaID;
        public ModificarMateriasPrimas(int MateriaPrimaID)
        {
            InitializeComponent();
            ListaTiposMateriales();
            mostrarInformacionMateriasPrimas(MateriaPrimaID);
        }

        private void mostrarInformacionMateriasPrimas(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/MateriasPrimas/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<MateriasPrimasListView>(response.data.ToString());
                    Materia_PrimaID = listaView.Materia_PrimaID;
                    PartNo.Text = listaView.PartNo;
                    Nombre_Materia_Prima.Text = listaView.Descripcion;
                    Descripcion.Text = listaView.Descripcion;

                }

            }
        }

        private async void ListaTiposMateriales()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposMateriales/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposMaterialesListView>>(response.data.ToString());

                    listaTiposMateriales.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }
    }
}