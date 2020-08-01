using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Models.MateriasPrimas;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.MateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarMateriasPrimas : ContentPage
    {
        GestionarMateriasPrimasViewModel GestionarMateriasPrimasViewModel;
        public GestionarMateriasPrimas()
        {
            InitializeComponent();
            BindingContext = this.GestionarMateriasPrimasViewModel = new GestionarMateriasPrimasViewModel();
            ListaMateriasPrimas();
            agregarNuevasMateriasPrimas.Clicked += AgregarNuevasMateriasPrimas_Clicked;
            buscarMateriasPrimas.TextChanged += BuscarMateriasPrimas_TextChanged;
        }

        private void BuscarMateriasPrimas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarMateriasPrimas.Text == "")
            {
                ListaMateriasPrimas();
            }
            else
            {
                string PartNo = buscarMateriasPrimas.Text;
                string MateriaPrima = buscarMateriasPrimas.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/MateriasPrimas/ConsultarMateriasPrimasPorPartNoMateriaPrima/{PartNo}/{MateriaPrima}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<MateriasPrimasListView>>(response.data.ToString());

                            listaMateriasPrimas.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevasMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriasPrimas.RegistrarMateriasPrimas());
        }

        private async void ListaMateriasPrimas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/MateriasPrimas/MateriasPrimasList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<MateriasPrimasListView>>(response.data.ToString());

                    listaMateriasPrimas.ItemsSource = listaView;


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